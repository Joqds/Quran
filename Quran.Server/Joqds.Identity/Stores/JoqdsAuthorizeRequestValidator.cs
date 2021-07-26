using System.Threading.Tasks;
using IdentityServer4.Validation;

namespace Joqds.Identity.Stores
{
    internal class JoqdsAuthorizeRequestValidator : ICustomAuthorizeRequestValidator
    {
        public Task ValidateAsync(CustomAuthorizeRequestValidationContext context)
        {
            return Task.CompletedTask;
        }
    }
}