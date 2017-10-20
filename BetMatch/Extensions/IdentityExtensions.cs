using System.Security.Claims;
using System.Security.Principal;
using System;

namespace BetMatch.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetFirstName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("FirstName");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetLastName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("LastName");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static bool IsAdminApproved(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("IsAdminApproved");
            // Test for null to avoid issues during local testing
            return (claim == null) ? false : (String.Equals(claim.Value, "True") ? true : false);
        }
    }
}