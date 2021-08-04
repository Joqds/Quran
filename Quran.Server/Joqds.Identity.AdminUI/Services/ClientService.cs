using System.Collections.Generic;
using System.Linq;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Interfaces;

namespace Joqds.Identity.AdminUI.Services
{
    public class ClientService
    {
        private readonly IConfigurationDbContext _configurationDbContext;

        public ClientService(IConfigurationDbContext configurationDbContext)
        {
            _configurationDbContext = configurationDbContext;
        }

        public IEnumerable<Client> GetClients()
        {
            return _configurationDbContext.Clients.ToList();
        }
    }
}
