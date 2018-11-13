using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GigHub.Core.Models;
using FluentAssertions;

namespace GigHub.Tests.Models2
{
    [TestClass]
    public class NotificationTests
    {
        public void GigCanceled_WhenCalled_ShouldReturnNotificationForACanceledGig()
        {
            var gig = new Gig();
            var notification = Notification.GigCanceled(gig);

            notification.Type.Should().Be(NotificationType.GigCanceled);
            notification.Gig.Should().Be(gig);
        }
    }
}
