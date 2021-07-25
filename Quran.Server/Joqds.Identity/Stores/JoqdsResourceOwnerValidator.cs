using IdentityServer4.AspNetIdentity;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Validation;

using Microsoft.Extensions.Logging;

using Quran.Server.Infrastructure.Identity;
using Quran.Server.Infrastructure.Services;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Joqds.Identity.Stores
{
    public class JoqdsResourceOwnerValidator : ResourceOwnerPasswordValidator<ApplicationUser>
    {
        private readonly JoqdsUserManager _userManager;

        public JoqdsResourceOwnerValidator(JoqdsUserManager userManager, JoqdsSignInManager signInManager,
            ILogger<JoqdsResourceOwnerValidator> logger, IEventService eventService) : base(userManager, signInManager,
            eventService, logger)
        {
            _userManager = userManager;
        }

        public override async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var version = context.Request.ClientClaims.FirstOrDefault(x => x.Type == JoqdsConstants.ClaimTypes.Version)
                ?.Value;

            var joqdsClientType = Enum.Parse<JoqdsClientStore.JoqdsClientType>(context.Request.ClientId);
            //todo: check result has error step by step
            if (!ValidateVersion(version, joqdsClientType))
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.UnauthorizedClient,
                    ValidationResource.client_version_unsupported);
            }

            if (joqdsClientType == JoqdsClientStore.JoqdsClientType.None)
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidClient);

            if (await ValidateUserPassRequest(context)) await base.ValidateAsync(context);



            if (context.Result.Error != null)
                context.Result.Error = ValidationResource.ResourceManager.GetString(context.Result.Error) ??
                                       context.Result.Error;
            if (context.Result.IsError && string.IsNullOrEmpty(context.Result.ErrorDescription) &&
                context.Result.Error == ValidationResource.invalid_grant)
                context.Result.ErrorDescription = ValidationResource.invalid_username_or_password;
        }


        private async Task<bool> ValidateUserPassRequest(ResourceOwnerPasswordValidationContext context)
        {
            var normalizePhoneNumber = UtilitiesService.NormalizePhoneNumber(context.UserName);
            if (normalizePhoneNumber != null) context.UserName = normalizePhoneNumber;
            else
            {
                var user = await _userManager.FindByIdAsync(context.UserName);
                context.UserName = user.UserName;
            }

            return true;
        }

        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
        private bool ValidateVersion(string version, JoqdsClientStore.JoqdsClientType clientType)
        {
            return true;
        }

    }
}