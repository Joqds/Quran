using IdentityServer4.AspNetIdentity;

using Microsoft.Extensions.Logging;

using Quran.Server.Infrastructure.Identity;

namespace Joqds.Identity.Stores
{
    public class JoqdsProfileService : ProfileService<ApplicationUser>
    {

        public JoqdsProfileService(JoqdsUserManager userManager, JoqdsUserClaimsPrincipalFactory claimsFactory) : base(
            userManager, claimsFactory)
        {
        }

        public JoqdsProfileService(JoqdsUserManager userManager, JoqdsUserClaimsPrincipalFactory claimsFactory,
            ILogger<JoqdsProfileService> logger) : base(userManager, claimsFactory, logger)
        {
        }
    }
}