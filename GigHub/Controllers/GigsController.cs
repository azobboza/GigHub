using GigHub.Models;
using GigHub.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel 
            { 
                Genres = _context.Genres.ToList()
            };
            return View(viewModel);
        }
	}
}