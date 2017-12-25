using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using GigHub.Dtos;
using AutoMapper;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private ApplicationDbContext _context;
        public NotificationsController()
        {
            _context = new ApplicationDbContext();
        }


        public IEnumerable<NotificationDto> GetNewNotifications() 
        { 
            string userId = User.Identity.GetUserId();
            var notifications = _context.UserNotifications
                .Where(u => u.AttendeeId == userId && !u.IsRead)
                .Select(u => u.Notification)
                .Include(g => g.Gig.Artist)
                .ToList();

           
            //AutoMapper doesn't work
            //return notifications.Select(Mapper.Map<Notification, NotificationDto>); 

            //---------------------------
            //Replace with AutoMapper----
            //---------------------------
            return notifications.Select(n => new NotificationDto()
            {
                DateTime = n.DateTime,
                Gig = new GigDto
                {
                    Artist = new UserDto
                    {
                        Id = n.Gig.Artist.Id,
                        Name = n.Gig.Artist.Name
                    },
                    DateTime = n.Gig.DateTime,
                    Id = n.Gig.Id,
                    IsCanceled = n.Gig.IsCanceled,
                    Venue = n.Gig.Venue
                },
                OriginalDateTime = n.OriginalDateTime,
                OriginalVenue = n.OriginalVenue,
                Type = n.Type
            });
        }
    }
}
