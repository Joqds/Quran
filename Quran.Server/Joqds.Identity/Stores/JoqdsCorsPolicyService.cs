using System.Threading.Tasks;
using IdentityServer4.Services;

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