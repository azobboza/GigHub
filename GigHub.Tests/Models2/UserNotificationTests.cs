using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GigHub.Core.Models;
using FluentAssertions;

namespace GigHub.Tests.Models2
{
    /// <summary>
    /// Summary description for UserNotificationTests
    /// </summary>
    [TestClass]
    public class UserNotificationTests
    {
        [TestMethod]
        public void Read_WhenCalled_ShouldSetIsReadToTrue()
        {
            var userNotification = new UserNotification();
            userNotification.Read();

            userNotification.IsRead.Should().Be(true);
        }
    }
}
