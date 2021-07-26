using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Joqds.Identity.Tools
{
    public static class JoqdsIdentityExtensions
    {
        public static IEnumerable<string> GetScopes(this ClaimsPrincipal principal)
        {
            var scopeClaim = principal.Claims.Where(
                    c => string.Equals(c.Type, "scope", StringComparison.OrdinalIgnoreCase))
                .Select(x => x.Value)
                .ToList();

            return scopeClaim;
        }
    }
}