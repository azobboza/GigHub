﻿using GigHub.Models;
using GigHub.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public GigsController()
        {
            //questions
            //1. What about dependency injection
            //2. What about Repository patttern
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public ActionResult Search(GigViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel 
            { 
                Genres = _context.Genres.ToList(),
                Heading = "Add a gig"
            };
            return View("GigForm", viewModel);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {

            string userId = User.Identity.GetUserId();
            var gig = _context.Gigs.Single(g => g.Id == id && g.ArtistId == userId);
            var viewModel = new GigFormViewModel
            {
                Genres = _context.Genres.ToList(),
                Date = gig.DateTime.ToString("d MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Genre = gig.GenreId,
                Venue = gig.Venue,
                Heading = "Edit a gig",
                Id = gig.Id
            };
            return View("GigForm",viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }
                
            var gig = new Gig() 
            { 
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };

            _context.Gigs.Add(gig);
            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }

            string userId = User.Identity.GetUserId();
            var gig = _context.Gigs
                .Include(a => a.Attendances.Select(g => g.Attendee))
                .Single(g => g.Id == viewModel.Id && g.ArtistId == userId);

            gig.Modify(viewModel.GetDateTime(), viewModel.Venue, viewModel.Genre);
            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }

        public ActionResult Attending()
        {
            string userId = User.Identity.GetUserId();

            var gigs = _context.Attendances
                .Where(g => g.AttendeeId == userId)
                .Select(a => a.Gigs)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();

            var attendance = _context.Attendances
                .Where(a => a.AttendeeId == userId && a.Gigs.DateTime > DateTime.Now)
                .ToList()
                .ToLookup(a => a.GigId);

            var viewModel = new GigViewModel
            {
                UpcomingGigs = gigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm attending",
                Attendaces = attendance
            };

            return View("Gigs", viewModel);
        }

        public ActionResult Mine()
        {
            string userId = User.Identity.GetUserId();
            var gigs = _context.Gigs
                .Where(g => g.ArtistId == userId && 
                    g.DateTime > DateTime.Now && 
                    !g.IsCanceled)
                .Include(g => g.Genre)
                .ToList();

            return View(gigs);
        }

        public ActionResult Details(int id)
        {
            var gig = _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .SingleOrDefault(g => g.Id == id);

            if (gig == null)
                return HttpNotFound();

            var viewModel = new GigDetails { Gig = gig };

            if (User.Identity.IsAuthenticated)
            {
                string userId = User.Identity.GetUserId();
                viewModel.IsFollowing = _context.Following.Any(f => f.FollowerId == userId && f.FolloweeId == gig.ArtistId);
                viewModel.IsGoing = _context.Attendances.Any(a => a.GigId == gig.Id && a.AttendeeId == userId);
            }

            return View("GigDetails", viewModel);
        }
	}
}