using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace GigHub.Repositories
{
    public class GigRepository : GigHub.Repositories.IGigRepository
    {
        private readonly ApplicationDbContext _context;
        public GigRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Gig GetGig(int id) 
        {
            return _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .SingleOrDefault(g => g.Id == id);
        }

        public void Add(Gig gig) 
        {
            _context.Gigs.Add(gig);
        }

        public IEnumerable<Gig> GetGigUserAttending(string userId)
        {
            return _context.Attendances
                .Where(g => g.AttendeeId == userId)
                .Select(a => a.Gigs)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();
        }

        public Gig GetGigWithAttendeese(int gigId) 
        {
            return _context.Gigs
                .Include(a => a.Attendances.Select(g => g.Attendee))
                .Single(g => g.Id == gigId);
        }

        public IEnumerable<Gig> GetUpcomingGigsByArtist(string userId) 
        {
            return _context.Gigs
                   .Where(g => g.ArtistId == userId &&
                       g.DateTime > DateTime.Now &&
                       !g.IsCanceled)
                   .Include(g => g.Genre)
                   .ToList();   
        }

        public Gig GetGigWithArtistAndGenre(int gigId) 
        {
            return _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .SingleOrDefault(g => g.Id == gigId);
        }
    }
}