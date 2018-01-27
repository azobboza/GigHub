using GigHub.Models;
using System;
using System.Collections.Generic;
namespace GigHub.Repositories
{
    interface IAttendanceRepository
    {
        Attendance GetAttendace(int gigId, string userId);
        IEnumerable<GigHub.Models.Attendance> GetFutureAttendance(string userId);
    }
}
