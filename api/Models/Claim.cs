using System;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Api.Claims.Models
{
    /// <summary>
    /// Contract for Claim
    /// </summary>
    [DataContract]
    public partial class Claim : IEquatable<Claim>
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

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Claim {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Verified: ").Append(Verified).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Claim)obj);
        }

        /// <summary>
        /// Returns true if Claim instances are equal
        /// </summary>
        /// <param name="other">Instance of Claim to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Claim other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    Id == other.Id ||
                    Id.Equals(other.Id)
                ) && 
                (
                    Name == other.Name ||
                    Name != null &&
                    Name.Equals(other.Name)
                ) && 
                (
                    Verified == other.Verified ||
                    Verified != null &&
                    Verified.Equals(other.Verified)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                var hashCode = 41;
                // Suitable nullity checks etc, of course :)
                    if (Name != null)
                    hashCode = hashCode * 59 + Name.GetHashCode();
                    if (Verified != null)
                    hashCode = hashCode * 59 + Verified.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(Claim left, Claim right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Claim left, Claim right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
