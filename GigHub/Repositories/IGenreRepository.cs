using System;
using System.Collections.Generic;
namespace GigHub.Repositories
{
    interface IGenreRepository
    {
        IEnumerable<GigHub.Models.Genre> GetGenres();
    }
}
