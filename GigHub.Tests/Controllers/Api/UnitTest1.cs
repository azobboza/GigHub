using System;
using System.Web.Http.Results;
using FluentAssertions;
using GigHub.Controllers.Api;
using GigHub.Core;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GigHub.Tests.Controllers.Api
{
    [TestClass]
    public class UnitTest1
    {
        private AttendancesController _controller;
        private Mock<IAttendanceRepository> _mockRepository;
        private string _userId;
        private int _gigId = 1;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<IAttendanceRepository>();

            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.SetupGet(u => u.Attendance).Returns(_mockRepository.Object);

            _controller = new AttendancesController(mockUoW.Object);
            _userId = "1";
            _controller.MockCurrentUser(_userId, "user1@domain.com");
        }

        [TestMethod]
        public void BozaAttend_UserAttendingAGigForWhichHeHasAttendance_ShouldReturnBadRequest()
        {
            var attendances = new Attendance();
            _mockRepository.Setup(r => r.GetAttendace(_gigId, _userId)).Returns(attendances);

            var result = _controller.Attend(new AttendanceDto() { GigId = _gigId });

            result.Should().BeOfType<BadRequestResult>();
        }
    }
}
