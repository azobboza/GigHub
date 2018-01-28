using GigHub.Models;
using System;
using System.Collections.Generic;
namespace GigHub.Repositories
{
    public interface IGigRepository
    {
        void Add(GigHub.Models.Gig gig);
        Gig GetGig(int id);
        IEnumerable<GigHub.Models.Gig> GetGigUserAttending(string userId);
        Gig GetGigWithArtistAndGenre(int gigId);
        Gig GetGigWithAttendeese(int gigId);
        IEnumerable<GigHub.Models.Gig> GetUpcomingGigsByArtist(string userId);
    }
}
