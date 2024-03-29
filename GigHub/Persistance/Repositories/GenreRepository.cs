﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GigHub.Core.Repositories;
using GigHub.Core.Models;
using GigHub.Persistance;

namespace GigHub.Persistance.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext _context;
        public GenreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Genre> GetGenres() 
        {
            return _context.Genres.ToList();
        }
    }
}