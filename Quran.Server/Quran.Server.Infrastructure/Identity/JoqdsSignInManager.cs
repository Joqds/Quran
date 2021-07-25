using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Quran.Server.Infrastructure.Identity
{
    public class JoqdsSignInManager : SignInManager<ApplicationUser> {

        public JoqdsUserManager JoqdsUserManager { get; }
        public JoqdsSignInManager(JoqdsUserManager userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<JoqdsSignInManager> logger, IAuthenticationSchemeProvider schemes, IUserConfirmation<ApplicationUser> confirmation) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
            JoqdsUserManager = userManager;
        }

        public async Task<bool> ValidateCredentials(string username, string password)
        {
            var user = await JoqdsUserManager.FindByUsername(username);
            return user != null && await CanSignInAsync(user) && (!await JoqdsUserManager.HasPasswordAsync(user) || await JoqdsUserManager.CheckPasswordAsync(user, password));
        }
        
    }
}