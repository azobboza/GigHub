﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GigHub.Core.Models;
using GigHub.Persistance;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers
{
    public class FolloweeController : Controller
    {
        private ApplicationDbContext _context;

        public FolloweeController()
        {
            _context = new ApplicationDbContext();
        }
        //
        // GET: /Followee/
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            var artist = _context.Following
                .Where(f => f.FollowerId == userId)
                .Select(f => f.Followee)
                .ToList();

            return View(artist);
        }
	}
}