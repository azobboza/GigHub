using GigHub.Core.Models;
using System;
using System.Collections.Generic;
namespace GigHub.Core.Repositories
{
    public interface IAttendanceRepository
    {
        Attendance GetAttendace(int gigId, string userId);
        IEnumerable<Attendance> GetFutureAttendance(string userId);
        void Add(Attendance attendance);
        void Remove(Attendance attendance);
    }
}
