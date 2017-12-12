﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GigHub.Models;
using Microsoft.AspNet.Identity;

namespace GigHub.ViewModels
{
    public class GigViewModel
    {
        public IEnumerable<Gig> UpcomingGigs { get; set; }
        public bool ShowActions { get; set; }
    }
}