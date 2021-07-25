using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

using Quran.Server.Infrastructure.Identity;

using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Joqds.Identity.Stores
{
    public class JoqdsUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser>
    {
        private readonly JoqdsUserManager _userManager;

        public JoqdsUserClaimsPrincipalFactory(JoqdsUserManager userManager, IOptions<IdentityOptions> optionsAccessor)
            : base(userManager,
                optionsAccessor)
        {
            _userManager = userManager;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var generateClaims = await base.GenerateClaimsAsync(user);
            var claims = new List<Claim>();

            var rolesAsync = await UserManager.GetRolesAsync(user);
            claims.AddRange(rolesAsync.Select(x => new Claim(JoqdsConstants.ClaimTypes.Role, x)));

            var result = new ClaimsIdentity(generateClaims.Claims.Concat(claims),
                generateClaims.AuthenticationType, generateClaims.NameClaimType,
                generateClaims.RoleClaimType)
            {
                Actor = generateClaims.Actor,
                BootstrapContext = generateClaims.BootstrapContext,
                Label = generateClaims.Label,
            };
            return result;
        }
    }
}