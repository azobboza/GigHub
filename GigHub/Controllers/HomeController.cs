﻿using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _contex;
        public HomeController()
        {
            _contex = new ApplicationDbContext();
        }
        public ActionResult Index(string query = null)
        {
            var upcomingGigs = _contex.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .Where(g => g.DateTime > DateTime.Now && !g.IsCanceled);

            if (!string.IsNullOrWhiteSpace(query))
            {
                upcomingGigs = upcomingGigs.Where(g =>
                                                    g.Artist.Name.Contains(query) ||
                                                    g.Venue.Contains(query) ||
                                                    g.Genre.Name.Contains(query));
            }

            string userId = User.Identity.GetUserId();
            var attendance = _contex.Attendances
                .Where(a => a.AttendeeId == userId && a.Gigs.DateTime > DateTime.Now)
                .ToList()
                .ToLookup(a => a.GigId);


            var viewModel = new GigViewModel
            {
                UpcomingGigs = upcomingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gigs",
                SearchTerm = query,
                Attendaces = attendance
            };

            return View("Gigs", viewModel);
        }

        public ActionResult Details(int id)
        {
            return RedirectToAction("Details", "Gigs", new { id = id });
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}