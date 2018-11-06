using GigHub.Core.Models;
using System;
using System.Collections.Generic;
namespace GigHub.Core.Repositories
{
    public interface IGigRepository
    {
        void Add(Gig gig);
        Gig GetGig(int id);
        IEnumerable<Gig> GetGigUserAttending(string userId);
        Gig GetGigWithArtistAndGenre(int gigId);
        Gig GetGigWithAttendeese(int gigId);
        IEnumerable<Gig> GetUpcomingGigsByArtist(string userId);
    }
}
