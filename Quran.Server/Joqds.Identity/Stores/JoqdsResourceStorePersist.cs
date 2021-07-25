using IdentityServer4.EntityFramework.Interfaces;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.EntityFramework.Stores;
using IdentityServer4.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Joqds.Identity.Stores
{
    public class JoqdsResourceStorePersist : ResourceStore
    {
        private readonly JoqdsResourceStore _joqdsResourceStore;
        public JoqdsResourceStorePersist(IConfigurationDbContext context, ILogger<JoqdsResourceStorePersist> logger, JoqdsResourceStore joqdsResourceStore) : base(context, logger)
        {
            _joqdsResourceStore = joqdsResourceStore;
        }

        public override async Task<IEnumerable<ApiResource>> FindApiResourcesByNameAsync(IEnumerable<string> apiResourceNames)
        {
            var resourceNames = apiResourceNames.ToList();
            var resources = await base.FindApiResourcesByNameAsync(resourceNames);
            var apiResources = resources.ToList();
            if (apiResources.Any(x => resourceNames.Contains(x.Name)))
            {
                resources = await _joqdsResourceStore.FindApiResourcesByNameAsync(resourceNames);
                await Context.ApiResources.AddRangeAsync(resources.Select(x => x.ToEntity()));
                await ((DbContext)Context).SaveChangesAsync();
            }
            return apiResources;
        }

        public override async Task<IEnumerable<ApiResource>> FindApiResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            var names = scopeNames.ToList();
            var findApiResourcesByScopeNameAsync = await base.FindApiResourcesByScopeNameAsync(names);
            var findApiResourcesByNameAsync = await _joqdsResourceStore.FindApiResourcesByScopeNameAsync(names);
            var apiResources = findApiResourcesByNameAsync.Where(x => findApiResourcesByScopeNameAsync.All(y => y.Name != x.Name)).ToList();
            if (apiResources.Any())
            {
                await Context.ApiResources.AddRangeAsync(apiResources.Select(x => x.ToEntity()));
                await ((DbContext)Context).SaveChangesAsync();
            }

            var result = findApiResourcesByScopeNameAsync.ToList();
            result.AddRange(apiResources);
            return result;
        }

        public override async Task<IEnumerable<ApiScope>> FindApiScopesByNameAsync(IEnumerable<string> scopeNames)
        {
            var names = scopeNames.ToList();
            var findApiScopesByNameAsync = await base.FindApiScopesByNameAsync(names);
            var apiScopesByNameAsync = await _joqdsResourceStore.FindApiScopesByNameAsync(names);
            var apiScopes = apiScopesByNameAsync.Where(x => findApiScopesByNameAsync.All(y => y.Name != x.Name)).ToList();
            if (apiScopes.Any())
            {
                await Context.ApiScopes.AddRangeAsync(apiScopes.Select(x => x.ToEntity()));
                await ((DbContext)Context).SaveChangesAsync();
            }

            var result = findApiScopesByNameAsync.ToList();
            result.AddRange(apiScopes);

            return result;
        }

        public override async Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            var names = scopeNames.ToList();
            var findIdentityResourcesByScopeNameAsync = await base.FindIdentityResourcesByScopeNameAsync(names);
            var identityResourcesByScopeNameAsync = await _joqdsResourceStore.FindIdentityResourcesByScopeNameAsync(names);
            var identityResources = identityResourcesByScopeNameAsync.Where(x => findIdentityResourcesByScopeNameAsync.All(y => y.Name != x.Name)).ToList();
            if (identityResources.Any())
            {
                await Context.IdentityResources.AddRangeAsync(identityResources.Select(x => x.ToEntity()));
                await ((DbContext)Context).SaveChangesAsync();
            }
            var result = findIdentityResourcesByScopeNameAsync.ToList();
            result.AddRange(identityResources);
            return result;
        }

        public override async Task<Resources> GetAllResourcesAsync()
        {
            var allResourcesAsync = await base.GetAllResourcesAsync();
            return allResourcesAsync;
        }
    }
}