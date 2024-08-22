using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Api.Claims.Attributes;
using Api.Claims.Models;

namespace Api.Claims.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class ClaimsApiController : ControllerBase
    { 
        /// <summary>
        /// Get a claim by ID
        /// </summary>
        /// <remarks>Retrieves a claim from the database based on its ID.</remarks>
        /// <param name="id">The ID of the claim to retrieve.</param>
        /// <response code="200">The claim was found.</response>
        /// <response code="404">The claim was not found.</response>
        [HttpGet]
        [Route("/api/claims/{id}")]
        [ValidateModelState]
        [SwaggerOperation("ClaimsIdGet")]
        [SwaggerResponse(statusCode: 200, type: typeof(Claim), description: "The claim was found.")]
        public virtual IActionResult ClaimsIdGet([FromRoute][Required]int? id)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(Claim));

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404);
            string exampleJson = null;
            exampleJson = "{\n  \"claimName\" : \"claimName\",\n  \"verified\" : true,\n  \"id\" : 0\n}";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<Claim>(exampleJson)
                        : default(Claim);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// Submit a batch of claims
        /// </summary>
        /// <remarks>Submits a batch of claims, checks for duplicates, and stores new claims in the database. Returns a list of already registered claims if any.</remarks>
        /// <param name="body"></param>
        /// <response code="200">Claims processed successfully.</response>
        [HttpPost]
        [Route("/api/claims")]
        [ValidateModelState]
        [SwaggerOperation("ClaimsPost")]
        [SwaggerResponse(statusCode: 200, type: typeof(InlineResponse200), description: "Claims processed successfully.")]
        public virtual IActionResult ClaimsPost([FromBody]List<Claim> claims)
        { 
            List<Claim> uniqueClaims = new List<Claim>();
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

            // TODO Add to queue
            var response = new 
            {
                duplicatedClaims = duplicateClaims,
            };

            return new ObjectResult(response);
        }
    }
}
