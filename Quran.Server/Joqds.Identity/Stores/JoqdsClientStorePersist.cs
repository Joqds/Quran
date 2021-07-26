using IdentityServer4.EntityFramework.Interfaces;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.EntityFramework.Stores;
using IdentityServer4.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System.Threading.Tasks;

namespace Joqds.Identity.Stores
{
    public class JoqdsClientStorePersist : ClientStore
    {
        private readonly IConfigurationDbContext _context;
        private readonly JoqdsClientStore _joqdsClientStore;
        public JoqdsClientStorePersist(IConfigurationDbContext context, ILogger<ClientStore> logger, JoqdsClientStore joqdsClientStore) : base(context, logger)
        {
            _context = context;
            _joqdsClientStore = joqdsClientStore;
        }

        public override async Task<Client> FindClientByIdAsync(string clientId)
        {
            var client = await base.FindClientByIdAsync(clientId);
            if (client == null)
            {
                client = await _joqdsClientStore.FindClientByIdAsync(clientId);
                if (client == null) return null;
                await _context.Clients.AddAsync(client.ToEntity());
                await ((DbContext)_context).SaveChangesAsync();
            }
            return client;
        }
    }
}