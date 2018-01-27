﻿using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Repositories
{
    public class FollowingRepository : GigHub.Repositories.IFollowingRepository
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