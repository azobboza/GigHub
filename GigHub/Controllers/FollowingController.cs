using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GigHub.Dtos;
using Microsoft.AspNet.Identity;


namespace GigHub.Controllers
{
    public class FollowingController : ApiController
    {
        private ApplicationDbContext _context;

        public FollowingController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Following(FollowingDto dto) 
        {   
            string userId = User.Identity.GetUserId();
            if (_context.Following.Any(f => f.UserId == userId && f.ArtistId == dto.ArtistId))
                return BadRequest("Following already exists");

            var following = new Following
            {
                UserId = userId,
                ArtistId = dto.ArtistId
            };

            _context.Following.Add(following);
            _context.SaveChanges();

            return Ok();
        }
    }
}
