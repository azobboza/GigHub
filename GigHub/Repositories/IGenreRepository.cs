using System;
using System.Collections.Generic;
namespace GigHub.Repositories
{
    public interface IGenreRepository
    {
        IEnumerable<GigHub.Models.Genre> GetGenres();
    }
}
