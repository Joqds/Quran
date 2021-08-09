using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.AspNetIdentity;
using IdentityServer4.Models;
using Microsoft.Extensions.Logging;

using Quran.Server.Infrastructure.Identity;

namespace Joqds.Identity.Stores
{
    public class JoqdsProfileService : ProfileService<ApplicationUser>
    {
        private readonly JoqdsUserManager _userManager;
        private readonly JoqdsUserClaimsPrincipalFactory _claimsFactory;
        public JoqdsProfileService(JoqdsUserManager userManager, JoqdsUserClaimsPrincipalFactory claimsFactory) : base(
            userManager, claimsFactory)
        {
            _userManager = userManager;
            _claimsFactory = claimsFactory;
        }

        public JoqdsProfileService(JoqdsUserManager userManager, JoqdsUserClaimsPrincipalFactory claimsFactory,
            ILogger<JoqdsProfileService> logger) : base(userManager, claimsFactory, logger)
        {
            _userManager = userManager;
            _claimsFactory = claimsFactory;
        }

        public override async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            await base.GetProfileDataAsync(context);
//            int x = 1;
            var user = await _userManager.GetUserAsync(context.Subject);
            if (user.EmailConfirmed && context.RequestedClaimTypes.Any(x => x == OidcConstants.StandardScopes.Email))
                context.IssuedClaims.Add(new Claim(OidcConstants.StandardScopes.Email, user.Email));
            if (user.PhoneNumberConfirmed && context.RequestedClaimTypes.Any(x => x == "phone_number"))
                context.IssuedClaims.Add(new Claim(OidcConstants.StandardScopes.Phone, user.PhoneNumber));


            return;

        }
    }
}