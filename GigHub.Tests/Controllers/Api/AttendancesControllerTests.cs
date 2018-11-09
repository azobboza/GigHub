using FluentAssertions;
using GigHub.Controllers.Api;
using GigHub.Core;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Http.Results;

namespace GigHub.Tests.Controllers.Api
{
    [TestClass]
    public class AttendancesControllerTests
    {

        private const string _userId = "1";
        private const string _username = "user1@domain.com";
        private int _gigId = 1;

        private AttendancesController _controller;
        private Mock<IAttendanceRepository> _mockRepository;

        [TestInitialize]
        public void Testinitialize()
        {
            _mockRepository = new Mock<IAttendanceRepository>();
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.SetupGet(r => r.Attendance).Returns(_mockRepository.Object);

            _controller = new AttendancesController(mockUoW.Object);
            _controller.MockCurrentUser(_userId, _username);
        }

        [TestMethod]
        public void Attend_UserAttendingAGigForWhichHeHasAttendance_ShouldReturnBadRequest()
        {
            var attendances = new Attendance();
            _mockRepository.Setup(r => r.GetAttendace(_gigId, _userId)).Returns(attendances);

            var result = _controller.Attend(new AttendanceDto() { GigId = _gigId });

            result.Should().BeOfType<BadRequestErrorMessageResult>();
        }

        [TestMethod]
        public void Attend_ValidRequest_ShouldReturnOK()
        {
            var attendance = new Attendance();
            _mockRepository.Setup(r => r.GetAttendace(_gigId, _userId)).Returns(() => null);

            var result = _controller.Attend(new AttendanceDto() { GigId = _gigId });

            result.Should().BeOfType<OkResult>();
        }

        [TestMethod]
        public void DeleteAttendance_AttendacesHasNoExists_ShouldReturnNotFound()
        {
            var attendace = new Attendance();
            _mockRepository.Setup(r => r.GetAttendace(_gigId, _userId)).Returns(() => null);

            var result = _controller.DeleteAttendance(_gigId);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void DeleteAttendance_ValidRequest_ShouldReturnOK()
        {
            var attendace = new Attendance() { GigId = _gigId, AttendeeId = _userId };
            _mockRepository.Setup(r => r.GetAttendace(_gigId, _userId)).Returns(attendace);

            var result = _controller.DeleteAttendance(_gigId);

            result.Should().BeOfType<OkNegotiatedContentResult<int>>();
        }

        /// <summary>
        /// ?!?!?!?!?! Checking what's this "result.Content.Should().Be(1);" and "OkNegotiatedContentResult"
        /// </summary>
        [TestMethod]
        public void DeleteAttendance_ValidRequest_ShouldReturnTheIdOfDeletedAttendance()
        {
            var attendace = new Attendance() { GigId = _gigId, AttendeeId = _userId };
            _mockRepository.Setup(r => r.GetAttendace(_gigId, _userId)).Returns(attendace);

            var result = (OkNegotiatedContentResult<int>)_controller.DeleteAttendance(1);

            result.Content.Should().Be(1);
        }
    }
}
