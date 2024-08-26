using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using Prometheus;
using Api.Claims.Models;
using System;

namespace Api.Claims.Controllers
{
    /// <summary>
    /// ClaimsApiController
    /// </summary>
    [ApiController]
    public class ClaimsApiController : ControllerBase
    {
        private readonly ClaimsDBContext _dbContext;
        private readonly ILogger<ClaimsApiController> _logger;

 
        private static readonly Histogram ClaimCountPerRequestHistogram = Metrics.CreateHistogram(
            "claim_count_per_request",
            "Number of claims sent per request",
            new HistogramConfiguration
            {
                LabelNames = new[] { "endpoint" }
            });

        public ClaimsApiController(ClaimsDBContext dbContext, ILogger<ClaimsApiController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Get a claim by ID
        /// </summary>
        /// <remarks>Retrieves a claim from the database based on its ID.</remarks>
        /// <param name="id">The ID of the claim to retrieve.</param>
        /// <response code="200">The claim was found.</response>
        /// <response code="404">The claim was not found.</response>
        [HttpGet]
        [Route("/api/claims/{id}")]
        [SwaggerOperation("ClaimsIdGet")]
        [SwaggerResponse(statusCode: 200, type: typeof(Claim), description: "The claim was found.")]
        public virtual IActionResult ClaimsIdGet([FromRoute][Required]int? id)
        { 
            var claim = _dbContext.Claims.Find(id);

            if (claim == null)
            {
                return NotFound();
            }

            return Ok(claim); 
        }

        /// <summary>
        /// Submit a batch of claims
        /// </summary>
        /// <remarks>Submits a batch of claims, checks for duplicates, and stores new claims in the database. Returns a list of already registered claims if any.</remarks>
        /// <response code="200">Claims processed successfully.</response>
        [HttpPost]
        [Route("/api/claims")]
        [SwaggerOperation("ClaimsPost")]
        [SwaggerResponse(statusCode: 200, type: typeof(ClaimsPostResponse), description: "Claims processed successfully.")]
        public virtual IActionResult ClaimsPost([FromBody]List<Claim> claims)
        { 
            ClaimCountPerRequestHistogram.WithLabels("/api/claims").Observe(claims.Count);

            List<Claim> uniqueClaims    = new List<Claim>();
            List<Claim> duplicateClaims = new List<Claim>();

            HashSet<int> seenIds = new HashSet<int>();
            

            foreach (var claim in claims)
            {
                if (seenIds.Add(claim.Id)) 
                {
                    uniqueClaims.Add(claim);
                }
                else
                {
                    duplicateClaims.Add(claim);
                }
            }

            foreach (var claim in uniqueClaims)
            {
                try
                {
                    _dbContext.Claims.Add(claim);
                    _dbContext.SaveChanges();
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex, "Error saving claim with ID {ClaimId}. Exception: {ExceptionMessage}", claim.Id, ex.Message);
                    duplicateClaims.Add(claim);
                }
            }

            var response = new ClaimsPostResponse()
            {
                DuplicatedClaims = duplicateClaims,
            };

            return Ok(response);
        }
    }
}