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
            if (_context.Following.Any(f => f.FollowerId == userId && f.FolloweeId == dto.ArtistId))
                return BadRequest("Following already exists");

            var following = new Following
            {
                FollowerId = userId,
                FolloweeId = dto.ArtistId
            };

            _context.Following.Add(following);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Unfollow(string id)
        {
            string userId = User.Identity.GetUserId();
            var following = _context.Following.Single(f => f.FollowerId == userId && f.FolloweeId == id);
            if (following == null)
                return NotFound();

            _context.Following.Remove(following);
            _context.SaveChanges();

            return Ok(id);
        }
    }
}
