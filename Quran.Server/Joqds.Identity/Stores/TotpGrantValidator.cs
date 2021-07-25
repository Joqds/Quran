using System;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Joqds.Identity.Tools;
using Quran.Server.Infrastructure.Identity;
using Quran.Server.Infrastructure.Services;

namespace Joqds.Identity.Stores
{
    public class TotpGrantValidator : IExtensionGrantValidator
    {
        private readonly JoqdsUserManager _userManager;
        private readonly ITotpValidator _totpValidator;
        


        public TotpGrantValidator(
            JoqdsUserManager userManager,
            ITotpValidator totpValidator)
        {
            _userManager = userManager;
            _totpValidator = totpValidator;
        }

        public string GrantType => JoqdsConstants.Grants.Totp;

        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            var username = context.Request.Raw.Get("username");
            var token = context.Request.Raw.Get("otp");

            if (!int.TryParse(token, out int tokenValue))
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, ValidationResource.invalid_username_or_totp);
                return;
            }

            ApplicationUser user=null;
            if (UtilitiesService.TryNormalizePhoneNumber(username,out string phone))
            {
                user = await _userManager.FindByPhoneNumber(phone);
            }
            if (user == null)
            {
                user = await _userManager.FindByIdAsync(username);
            }
            if (!_totpValidator.Validate(user?.SecurityStamp, tokenValue))
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, ValidationResource.invalid_username_or_totp);
                return;
            }

            context.Result = new GrantValidationResult(user?.Id.ToString(), JoqdsConstants.Grants.Totp, DateTime.UtcNow);
        }
    }
}