using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace AuthToken.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetUserEmail(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst(JwtRegisteredClaimNames.Email);
            if (claim == null)
            {
                return null;
            }

            return claim.Value;
        }

        public static string GetUserId(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst("UserId");
            if (claim == null)
            {
                return string.Empty;
            }

            return claim.Value;
        }
    }
}
