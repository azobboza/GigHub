using System;
using System.Collections.Generic;
using System.Data.Entity;
using FluentAssertions;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
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

        [TestInitialize]
        public void TestInitialize()
        {
            _mockContext = new Mock<IApplicationDbContext>();
            mockGig = new Mock<DbSet<Gig>>();
            _mockContext.SetupGet(r => r.Gigs).Returns(() => mockGig.Object);
            
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
    }
}
