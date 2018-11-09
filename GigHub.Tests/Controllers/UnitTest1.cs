using System;
using System.Web.Http.Results;
using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using GigHub.Controllers.Api;
using FluentAssertions;

namespace GigHub.Tests.Controllers
{
    [TestClass]
    public class UnitTest1
    {
        private const string _userId = "1";
        private const string _username = "user1@domain.com";
        private int gigId = 1;

        private GigsController _controller;
        private Mock<IGigRepository> _mockGigRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockGigRepository = new Mock<IGigRepository>();
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.SetupGet(u => u.Gigs).Returns(_mockGigRepository.Object);

            _controller = new GigsController(mockUoW.Object);
            //_controller.MockCurrentUser(_userId, _username);
        }

        [TestMethod]
        public void Cancel_UserCancelingAnotherAnotherUsersGig_ShouldReturnUnauthorized()
        {
            var gig = new Gig() { ArtistId = _userId + "-Boza" };

            _mockGigRepository.Setup(r => r.GetGigWithAttendeese(gigId)).Returns(gig);

            var result = _controller.Cancel(gigId);
            result.Should().BeOfType<UnauthorizedResult>();
        }
    }
}
