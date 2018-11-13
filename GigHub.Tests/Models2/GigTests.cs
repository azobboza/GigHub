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
    /// Summary description for GigTests
    /// </summary>
    [TestClass]
    public class GigTests
    {
        [TestMethod]
        public void Cancel_WhenCalled_ShouldSetIsCanceledFieledToTrue()
        {
            var gig = new Gig();

            gig.Cancel();

            //one way
            //Assert.IsTrue(gig.IsCanceled);

            //second way
            gig.IsCanceled.Should().BeTrue();
        }

        [TestMethod]
        public void Cancel_WhenCalled_EachAttendeShouldHaveANotification()
        {
            var gig = new Gig();
            gig.Attendances.Add(new Attendance{ Attendee = new ApplicationUser { } });

            gig.Cancel();

            var attendees = gig.Attendances.Select(a => a.Attendee).ToList();
            attendees[0].UserNotifications.Count.Should().Be(1);
        }
    }
}
