using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using GigHub.Dtos;

namespace GigHub.Controllers
{
    [AllowAnonymous]
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _context;
        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }
        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            string userId = User.Identity.GetUserId();
            if (_context.Attendances.Any(a => a.GigId == dto.GigId && a.AttendeeId == userId))
                return BadRequest("The attendance already exists!");

            var attendances = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = userId
            };

            _context.Attendances.Add(attendances);
            _context.SaveChanges();

            return Ok();
        }
    }
}
