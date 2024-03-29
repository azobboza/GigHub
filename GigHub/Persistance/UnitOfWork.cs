﻿using GigHub.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GigHub.Core.Repositories;
using GigHub.Core;

namespace GigHub.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IGigRepository Gigs { get; private set; }
        public IGenreRepository Genre { get; private set; }
        public IAttendanceRepository Attendance { get; private set; }
        public IFollowingRepository Following { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Gigs = new GigRepository(_context);
            Genre = new GenreRepository(_context);
            Attendance = new AttendanceRepository(_context);
            Following = new FollowingRepository(_context);
        }
        public void Complete() 
        {
            _context.SaveChanges();
        }
    }
}