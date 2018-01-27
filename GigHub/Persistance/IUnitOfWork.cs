using GigHub.Repositories;
using System;
namespace GigHub.Persistance
{
    interface IUnitOfWork
    {
        IAttendanceRepository Attendance { get; }
        IFollowingRepository Following { get; }
        IGenreRepository Genre { get; }
        IGigRepository Gigs { get; }
        void Complete();
    }
}
