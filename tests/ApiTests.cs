using Microsoft.AspNetCore.Mvc;
using Moq;
using Api.Claims.Controllers;
using Api.Claims.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Api.Claims.Tests
{
    public class ClaimsApiControllerTests
    {
        private readonly ClaimsApiController _controller;
        private readonly Mock<ILogger<ClaimsApiController>> _mockLogger;
        private readonly DbContextOptions<ClaimsDBContext> _dbContextOptions;

        public ClaimsApiControllerTests()
        {
            // Setup in-memory database
            _dbContextOptions = new DbContextOptionsBuilder<ClaimsDBContext>()
                .UseInMemoryDatabase("ClaimsDatabase")
                .Options;

            var dbContext = new ClaimsDBContext(_dbContextOptions);

            _mockLogger = new Mock<ILogger<ClaimsApiController>>();

            _controller = new ClaimsApiController(dbContext, _mockLogger.Object);
        }

        [Fact]
        public void ClaimsIdGet_ReturnsOkResult_WhenClaimExists()
        {
            // Arrange
            var claimId = 1;
            var claim = new Claim { Id = claimId, Name = "Test Claim", Verified = true };

            using (var dbContext = new ClaimsDBContext(_dbContextOptions))
            {
                dbContext.Claims.Add(claim);
                dbContext.SaveChanges();
            }

            // Act
            var result = _controller.ClaimsIdGet(claimId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedClaim = Assert.IsType<Claim>(okResult.Value);
            Assert.Equal(claimId, returnedClaim.Id);
        }

        [Fact]
        public void ClaimsIdGet_ReturnsNotFound_WhenClaimDoesNotExist()
        {
            // Act
            var result = _controller.ClaimsIdGet(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void ClaimsPost_ReturnsOkResult_WithDuplicatedClaimsInBatch()
        {

            using (var dbContext = new ClaimsDBContext(_dbContextOptions))
            {
                // Clear any existing data to ensure a clean state for the test
                dbContext.Claims.RemoveRange(dbContext.Claims);
                dbContext.SaveChanges();
            }
            // Arrange
            var newClaims = new List<Claim>
            {
                new Claim { Id = 1, Name = "Claim1", Verified = true },
                new Claim { Id = 1, Name = "DuplicatedClaim1", Verified = true },
                new Claim { Id = 2, Name = "Claim2", Verified = false },
                new Claim { Id = 3, Name = "Claim2-Duplicated", Verified = true }
            };

            // Act
            var result = _controller.ClaimsPost(newClaims);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ClaimsPostResponse>(okResult.Value);

            Assert.Single(response.DuplicatedClaims);

            Assert.Equal(1, response.DuplicatedClaims[0].Id);
        }
    }
}
