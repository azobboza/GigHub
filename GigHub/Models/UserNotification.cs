using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GigHub.Models
{
    public class UserNotification
    {
        [Key]
        [Column(Order = 1)]
        public string AttendeeId { get; private set; }

        [Key]
        [Column(Order = 2)]
        public int NotificationId { get; private set; }

        public ApplicationUser Attendee { get; private set; }
        public Notification Notification { get; private set; }

        public bool IsRead { get; private set; }

        public UserNotification() { }
        public UserNotification(ApplicationUser attendee, Notification notification)
        {
            if (attendee == null)
                throw new ArgumentNullException("users");

            if (notification == null)
                throw new ArgumentNullException("notification");

            Notification = notification;
            Attendee = attendee;
        }
    }
}