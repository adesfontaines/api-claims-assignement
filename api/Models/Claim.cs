using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Api.Claims.Models
{
    /// <summary>
    /// Contract for Claim
    /// </summary>
    [DataContract]
    public partial class Claim
    { 
        /// <summary>
        /// The unique ID of the claim.
        /// </summary>
        /// <value>The unique ID of the claim.</value>
        [Required]
        [DataMember(Name="id")]
        public int Id { get; set; }

        /// <summary>
        /// The name of the claim.
        /// </summary>
        /// <value>The name of the claim.</value>
        [Required]
        [DataMember(Name="name")]
        public string Name { get; set; }

        /// <summary>
        /// Whether the claim has been verified.
        /// </summary>
        /// <value>Whether the claim has been verified.</value>
        [Required]
        [DataMember(Name="verified")]
        public bool? Verified { get; set; }
    }
}
