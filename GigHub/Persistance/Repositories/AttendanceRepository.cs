using System;
using System.Collections.Generic;
using System.Linq;
using GigHub.Core.Repositories;
using GigHub.Core.Models;

namespace GigHub.Persistance.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly IApplicationDbContext _context;
        public AttendanceRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Attendance> GetFutureAttendance(string userId) 
        {
            return _context.Attendances
                .Where(a => 
                    a.AttendeeId == userId && 
                    a.Gigs.DateTime > DateTime.Now)
                .ToList();
        }

        public Attendance GetAttendace(int gigId, string userId) 
        {
            return _context.Attendances
                .Where(a => 
                    a.GigId == gigId && 
                    a.AttendeeId == userId)
                .SingleOrDefault();
        }

        public void Add(Attendance attendance)
        {
            _context.Attendances.Add(attendance);
        }

        public void Remove(Attendance attendance)
        {
            _context.Attendances.Remove(attendance);
        }
    }
}