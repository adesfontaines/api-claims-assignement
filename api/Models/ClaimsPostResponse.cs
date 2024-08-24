using System.Collections.Generic;

namespace Api.Claims.Models
{
    /// <summary>
    /// Claims post response for bulk insert
    /// </summary>
    public class ClaimsPostResponse
    {
        public List<Claim> DuplicatedClaims { get; set; }
    }
}
