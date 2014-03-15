using System.Security.Claims;

namespace SuperMassive.Identity.TableStorage
{
    /// <summary>
    /// Encapsulates a claim to provides a serializable constructor
    /// </summary>
    public class IdentityUserClaim
    {
        public IdentityUserClaim()
        {
        }

        public IdentityUserClaim(Claim claim)
        {
            ClaimType = claim.Type;
            ClaimValue = claim.Value;
        }
        public IdentityUserClaim(string type, string value)
        {
            ClaimType = type;
            ClaimValue = value;
        }

        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}
