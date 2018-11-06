using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GigHub.Core.Repositories;
using GigHub.Core.Models;
using GigHub.Persistance;

namespace GigHub.Persistance.Repositories
{
    public class FollowingRepository : IFollowingRepository
    {
        private readonly ApplicationDbContext _context;
        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Following GetFollowing(string userId, string followeeId) 
        {
            return _context.Following.Where(f => f.FollowerId == userId && f.FolloweeId == followeeId).SingleOrDefault();
        }
    }
}