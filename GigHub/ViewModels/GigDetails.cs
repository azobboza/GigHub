using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.ViewModels
{
    public class GigDetails
    {
        public Gig Gig { get; set; }
        public bool IsFollowing { get; set; }
        public bool IsGoing { get; set; }

        //OLD way
        //public string ApplicationName { get; set; }
        //public bool Following { get; set; }
        //public bool Going { get; set; }
        //public string Venue { get; set; }
        //public string DateTime { get; set; }
        
        //public static string FormatPlace(string venue, DateTime datetime)
        //{
        //    return "Performing at " + venue + " on " + FormatDate(datetime) + " at " + FormatTime(datetime);
        //}

        //public static string FormatDate(DateTime datetime)
        //{
        //    return datetime.ToString("dd MMM");
        //}

        //public static string FormatTime(DateTime datetime)
        //{
        //    return datetime.ToString("HH:mm");
        //}
    }
}