using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Quran.Server.Infrastructure.Identity.Helper;

using System;
using System.Threading.Tasks;

namespace Quran.Server.Infrastructure.Identity.Implementations
{
    public class PasswordlessLoginTotpTokenProvider : DataProtectorTokenProvider<ApplicationUser>
    {
        private readonly ITotpValidator _totpValidator;
        private readonly ITotpGenerator _totpGenerator;

        public PasswordlessLoginTotpTokenProvider(
            IDataProtectionProvider dataProtectionProvider,
            IOptions<PasswordlessLoginTokenProviderOptions> options,
            ILogger<DataProtectorTokenProvider<ApplicationUser>> logger,
            ITotpGenerator totpGenerator,
            ITotpValidator totpValidator
        ) : base(dataProtectionProvider, options, logger)
        {
            _totpGenerator = totpGenerator;
            _totpValidator = totpValidator;
        }

        public override Task<bool> CanGenerateTwoFactorTokenAsync(
            UserManager<ApplicationUser> userManager,
            ApplicationUser user
        )
        {
            return Task.FromResult(false);
        }

        public override async Task<string> GenerateAsync(
            string purpose,
            UserManager<ApplicationUser> userManager,
            ApplicationUser user
        )
        {
            var securityStamp = await userManager.GetSecurityStampAsync(user);
            var token = _totpGenerator.GenerateString(securityStamp);
            return token;
        }

        public override async Task<bool> ValidateAsync(
            string purpose,
            string token,
            UserManager<ApplicationUser> userManager,
            ApplicationUser user
        )
        {
            var securityStamp = await userManager.GetSecurityStampAsync(user);
            if (!int.TryParse(token, out var tokenInt))
            {
                throw new ArgumentException(TotpConstants.TokenNumericError, nameof(token));
            }

            var validate = _totpValidator.Validate(securityStamp, tokenInt);
            return validate;
        }
    }
}
