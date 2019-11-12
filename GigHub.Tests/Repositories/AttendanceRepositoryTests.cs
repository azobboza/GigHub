using System;
using System.Data.Entity;
using FluentAssertions;
using GigHub.Core.Models;
using GigHub.Persistance;
using GigHub.Persistance.Repositories;
using GigHub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GigHub.Tests.Repositories
{
    [TestClass]
    public class AttendanceRepositoryTests
    {
        private AttendanceRepository _attendanceRepository;
        Mock<DbSet<Attendance>> mockAttandance;

        [TestInitialize]
        public void TestInitialize()
        {
            var _mockContext = new Mock<IApplicationDbContext>();
            mockAttandance = new Mock<DbSet<Attendance>>();
            _mockContext.SetupGet(r => r.Attendances).Returns(() => mockAttandance.Object);
            _attendanceRepository = new AttendanceRepository(_mockContext.Object);
        }

        [TestMethod]
        public void GetFutureAttendance_ThereIsNotFutureAttandanceForGivenUser_ShouldNotBeReturned()
        {
            string userId = "1";
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(-1), ArtistId = userId };
            var attandance = new Attendance() { AttendeeId = userId, Gigs = gig };

            mockAttandance.SetSource(new[] { attandance });

            var attendances = _attendanceRepository.GetFutureAttendance(userId);
            attendances.Should().BeEmpty();
        }

        [TestMethod]
        public void GetFutureAttendance_AttandancesIsForDifferentUser_ShouldNotBeReturned()
        {
            string userId = "1";
            var gig = new Gig { DateTime = DateTime.Now.AddDays(1), ArtistId = userId };
            var attendance = new Attendance { AttendeeId = userId, Gigs = gig };
            mockAttandance.SetSource(new[] { attendance });

            var attendances = _attendanceRepository.GetFutureAttendance(userId + "-");
            attendances.Should().BeEmpty();
        }

        [TestMethod]
        public void GetFutureAttendance_AttandanceForGivenUserAndGigIdExists_ShouldBeReturn()
        {
            string userId = "1";
            int gigId = 1;
            var attendance = new Attendance { AttendeeId = userId, GigId = gigId };

            mockAttandance.SetSource(new[] { attendance });
            var attendances = _attendanceRepository.GetAttendace(2, userId);

            attendances.Should().Be(attendance);
        }


    }
}
