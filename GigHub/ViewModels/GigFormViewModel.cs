using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.ViewModels
{
    public class GigFormViewModel
    {
        public string Vemue { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public int GenreId { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
    }
}