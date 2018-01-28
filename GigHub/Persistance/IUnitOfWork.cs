using GigHub.Repositories;
using System;
namespace GigHub.Persistance
{
    public interface IUnitOfWork
    {
        IAttendanceRepository Attendance { get; }
        IFollowingRepository Following { get; }
        IGenreRepository Genre { get; }
        IGigRepository Gigs { get; }
        void Complete();
    }
}
