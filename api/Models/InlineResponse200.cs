using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Api.Claims.Models
{
    /// <summary>
    /// InlineResponse200
    /// </summary>
    [DataContract]
    public partial class InlineResponse200 : IEquatable<InlineResponse200>
    { 
        /// <summary>
        /// List of claims that were already registered in the database.
        /// </summary>
        /// <value>List of claims that were already registered in the database.</value>

        [DataMember(Name="alreadyRegisteredClaims")]
        public List<Claim> AlreadyRegisteredClaims { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class InlineResponse200 {\n");
            sb.Append("  AlreadyRegisteredClaims: ").Append(AlreadyRegisteredClaims).Append("\n");
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
            return obj.GetType() == GetType() && Equals((InlineResponse200)obj);
        }

        /// <summary>
        /// Returns true if InlineResponse200 instances are equal
        /// </summary>
        /// <param name="other">Instance of InlineResponse200 to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(InlineResponse200 other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    AlreadyRegisteredClaims == other.AlreadyRegisteredClaims ||
                    AlreadyRegisteredClaims != null &&
                    AlreadyRegisteredClaims.SequenceEqual(other.AlreadyRegisteredClaims)
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
                    if (AlreadyRegisteredClaims != null)
                    hashCode = hashCode * 59 + AlreadyRegisteredClaims.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(InlineResponse200 left, InlineResponse200 right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(InlineResponse200 left, InlineResponse200 right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
