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
        
        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel 
            { 
                Genres = _context.Genres.ToList()
            };
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("Create", viewModel);
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

        public ActionResult Attending()
        {
            string userId = User.Identity.GetUserId();

            var gigs = _context.Attendances
                .Where(g => g.AttendeeId == userId)
                .Select(a => a.Gigs)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();

            var viewModel = new GigViewModel
            {
                UpcomingGigs = gigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm attending"
            };

            return View("Gigs", viewModel);
        }

        public ActionResult Mine()
        {
            string userId = User.Identity.GetUserId();
            var gigs = _context.Gigs
                .Where(g => g.ArtistId == userId && g.DateTime > DateTime.Now)
                .Include(g => g.Genre)
                .ToList();

            return View(gigs);
        }
	}
}