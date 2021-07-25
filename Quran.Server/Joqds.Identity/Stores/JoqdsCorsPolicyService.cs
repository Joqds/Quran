using IdentityServer4.Services;

using System.Threading.Tasks;

namespace Joqds.Identity.Stores
{
    public class JoqdsCorsPolicyService : ICorsPolicyService
    {
        public Task<bool> IsOriginAllowedAsync(string origin)
        {
            //todo: [identity] read from configuration
            return Task.FromResult(true);
        }
    }
}