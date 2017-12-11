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

            //string userId = User.Identity.GetUserId();
            string currentUseerId = "ea73b12e-5ed4-4b20-ac63-a817fb5c6777";

            if (_context.Attendances.Any(a => a.GigId == dto.GigId && a.AttendeeId == currentUseerId))
                return BadRequest("The attendance already exists!");

            var attendances = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = currentUseerId
            };

            _context.Attendances.Add(attendances);
            _context.SaveChanges();

            return Ok("ovo je okej");
        }
    }
}
