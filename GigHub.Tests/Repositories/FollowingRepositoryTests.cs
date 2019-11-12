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
    public class UnitTest1
    {
        private Mock<IApplicationDbContext> _mockContext;
        private FollowingRepository _followingRepository;
        Mock<DbSet<Following>> mockFollowing;

        [TestInitialize]
        public void TestInitialize()
        {
            mockFollowing = new Mock<DbSet<Following>>();
            _mockContext = new Mock<IApplicationDbContext>();
            _mockContext.SetupGet(r => r.Following).Returns(() => mockFollowing.Object);
            _followingRepository = new FollowingRepository(_mockContext.Object);
        }

        [TestMethod]
        public void GetFollowing_XXX_XXX()
        {
            string userId = "1";
            string followId = "1";
            var follow = new Following { FollowerId = userId, FolloweeId = followId };
            mockFollowing.SetSource(new[] { follow });

            var followReturn = _followingRepository.GetFollowing(userId + "2", followId);

            followReturn.Should().Be(follow);
        }
    }
}
