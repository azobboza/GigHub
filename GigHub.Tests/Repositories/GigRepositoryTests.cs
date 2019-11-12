using System;
using System.Collections.Generic;
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
    public class GigRepositoryTests
    {
        private Mock<IApplicationDbContext> _mockContext;
        private GigRepository _gigRepository;
        
        Mock<DbSet<Gig>> mockGig;
        Mock<DbSet<Attendance>> mockAtttendace;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockContext = new Mock<IApplicationDbContext>();
            mockGig = new Mock<DbSet<Gig>>();
            mockAtttendace = new Mock<DbSet<Attendance>>();
            _mockContext.SetupGet(r => r.Gigs).Returns(() => mockGig.Object);
            _mockContext.SetupGet(r => r.Attendances).Returns(() => mockAtttendace.Object);
            
            _gigRepository = new GigRepository(_mockContext.Object);
        }

        //it has solved the problem!!!
        //https://codingoncaffeineblog.wordpress.com/2017/10/23/how-to-mock-an-entity-framework-dbcontext-and-dbset-with-moq/

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsInThePast_ShouldNotToBeReturned()
        {
            string userId = "1";
            //arrange
            var gig = new Gig { DateTime = DateTime.Now.AddDays(-1), ArtistId = userId };
            mockGig.SetSource(new List<Gig> { gig });
            //act
            var gigs = _gigRepository.GetUpcomingGigsByArtist(userId);

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsCanceled_ShouldNotBeReturned()
        {
            string userId = "1";
            var gig = new Gig { DateTime = DateTime.Now.AddDays(1), ArtistId = userId };
            gig.Cancel();

            mockGig.SetSource(new[] { gig });
            var gigs = _gigRepository.GetUpcomingGigsByArtist(userId);

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsForDifferentArtist_ShouldNotBeReturned()
        {
            var gig = new Gig() { ArtistId = "1", DateTime = DateTime.Now.AddDays(1) };
            mockGig.SetSource(new[] { gig});

            var gigs = _gigRepository.GetUpcomingGigsByArtist("2");
            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsForGivenArtistAndIsInFuture_ShoulBeReturned()
        {
            string userId = "1";

            var gig = new Gig() { DateTime = DateTime.Now.AddDays(1), ArtistId = userId };
            mockGig.SetSource(new[] { gig });

            var gigs = _gigRepository.GetUpcomingGigsByArtist(userId);

            gigs.Should().Contain(gig);
        }

        [TestMethod]
        public void GetGigUserAttending_ArtistHasFutureAttending_ShouldBeReturned()
        {
            string userId = "1";
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(1), ArtistId = userId };

            var attendance = new Attendance() { Gigs = gig, AttendeeId = userId };
            mockAtttendace.SetSource(new[] { attendance });

            var gigs = _gigRepository.GetGigUserAttending(userId);

            gigs.Should().Contain(gig);
        }

        [TestMethod]
        public void GetGigUserAttending_UserAttendingIsForDifferentUser_ShouldNotBeReturned()
        {
            string userId = "1";
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(1), ArtistId = userId };
            var attandance = new Attendance() { Gigs = gig, AttendeeId = userId };

            mockAtttendace.SetSource(new[] { attandance });
            var gigs = _gigRepository.GetGigUserAttending(userId + "-");

            gigs.Should().BeEmpty();
        }
    }
}
