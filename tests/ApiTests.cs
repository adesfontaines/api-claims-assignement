using Api.Claims.Controllers;
using Api.Claims.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using System.Collections.Generic;
using Xunit;

namespace Api.Tests
{
    public class ClaimsApiControllerTests
    {
        [Fact]
        public void ClaimsIdGet_WithValidId_ReturnsClaim() // Add parameters here
        {
            // Arrange
            var controller = new ClaimsApiController(); 

            // Act
            var result = controller.ClaimsIdGet(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
        }

        // Example unit test for ClaimsPost
        [Fact]
        public void ClaimsPost_WithDuplicateClaims_ReturnsDuplicateClaims()
        {
            // Arrange
            var controller = new ClaimsApiController();
            var claims = new List<Claim>
            {
                new Claim { Id = 1, Name = "claim-1", Verified = true },
                new Claim { Id = 2, Name = "claim-2", Verified = false },
                new Claim { Id = 1, Name = "duplicate-claim-1", Verified = false }, // Duplicate ID 1
                new Claim { Id = 2, Name = "duplicate-claim-2", Verified = true } // Duplicate ID 2
            };

            // Act
            var result = controller.ClaimsPost(claims);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            
            // Access the Value property of the OkObjectResult and cast it to ClaimsPostResponse
            var responseContent = Assert.IsType<ClaimsPostResponse>(okResult.Value);

            // Assert that there are 2 duplicate claims
            responseContent.DuplicatedClaims.Should().HaveCount(2);

            // Assert that the duplicate claims have the expected IDs (1 and 2)
            responseContent.DuplicatedClaims.Select(c => c.Id).Should().BeEquivalentTo(new[] { 1, 2 });
        }
    }
}