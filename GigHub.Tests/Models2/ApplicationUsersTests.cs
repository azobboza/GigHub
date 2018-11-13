using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GigHub.Core.Models;
using FluentAssertions;
using System.Linq;

namespace GigHub.Tests.Models2
{
    /// <summary>
    /// Summary description for ApplicationUsersTests
    /// </summary>
    [TestClass]
    public class ApplicationUsersTests
    {
        [TestMethod]
        public void Notify_WhenCalled_ShouldAddTheNotification()
        {            
            var user = new ApplicationUser();

            var notification = new Notification();
            user.Notify(notification);

            user.UserNotifications.Count().Should().Be(1);

            var userNotifocation = user.UserNotifications.First();
            userNotifocation.Notification.Should().Be(notification);
        }
    }
}
