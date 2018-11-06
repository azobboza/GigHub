using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using GigHub.Core.Repositories;
using GigHub.Core.Models;
using GigHub.Persistance;

namespace GigHub.Persistance.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDbContext _context;
        public AttendanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Attendance> GetFutureAttendance(string userId) 
        {
            return _context.Attendances.Where(a => a.AttendeeId == userId && a.Gigs.DateTime > DateTime.Now).ToList();
        }

        public Attendance GetAttendace(int gigId, string userId) 
        {
            return _context.Attendances.Where(a => a.GigId == gigId && a.AttendeeId == userId).SingleOrDefault();
        }
    }
}