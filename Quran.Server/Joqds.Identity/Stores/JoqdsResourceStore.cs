using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Stores;

using Quran.Server.Infrastructure.Identity;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Joqds.Identity.Stores
{
    public class JoqdsResourceStore : IResourceStore
    {
        static JoqdsResourceStore()
        {
            var apiScopes = new List<ApiScope>
            {
                new ApiScope(JoqdsConstants.Scope.QuranNotif, "can connect to notifications of quran"),
            };

            var apiResources = new List<ApiResource>
            {
                //todo: add description and name for consent pages
                new ApiResource(JoqdsConstants.ApiResources.QuranApp, "general quran APIs")
                {
                    ApiSecrets = {new Secret("quran-aA123456@!".Sha256())},
                },
                new ApiResource(JoqdsConstants.ApiResources.QuranAdmin, "administrate quran APIs")
                {
                    ApiSecrets = {new Secret("quran-aA123456@!".Sha256())}
                },
                new ApiResource(JoqdsConstants.ApiResources.Admin, "joqds admin APIs")
                {
                    ApiSecrets = {new Secret("quran-aA123456@!".Sha256())}
                },
                new ApiResource(JoqdsConstants.ApiResources.QuranAdmin, "quran admin APIs")
                {
                    ApiSecrets = {new Secret("quran-aA123456@!".Sha256())}
                },
                new ApiResource(JoqdsConstants.ApiResources.God, "god admin APIs")
            };

            //add default scopes with role and version enable
            apiScopes.AddRange(apiResources.Select(x => new ApiScope(x.Name, x.DisplayName,
                new[] { JoqdsConstants.ClaimTypes.Role, JoqdsConstants.ClaimTypes.Version })).ToList());

            //add scopes to their api resources
            apiResources.ForEach(x =>
                x.Scopes = apiScopes.Where(y => y.Name.StartsWith(x.Name)).Select(y => y.Name).ToList());

            //todo: can add fcm token here
            var identityResources = new List<IdentityResource>
            {
                new IdentityResources.Phone(),
                new IdentityResources.Address(),
                new IdentityResources.Email(),
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

            JoqdsResources = new Resources
            {
                ApiResources = apiResources,
                ApiScopes = apiScopes,
                IdentityResources = identityResources,
                OfflineAccess = true
            };
        }

        private static readonly Resources JoqdsResources;

        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeNameAsync(
            IEnumerable<string> scopeNames)
        {
            var identityResources = new List<IdentityResource>();
            foreach (var scopeName in scopeNames)
            {
                switch (scopeName.ToLower())
                {
                    case IdentityServerConstants.StandardScopes.OpenId:
                        identityResources.Add(new IdentityResources.OpenId());
                        break;
                    case IdentityServerConstants.StandardScopes.Phone:
                        identityResources.Add(new IdentityResources.Phone());
                        break;
                    case IdentityServerConstants.StandardScopes.Address:
                        identityResources.Add(new IdentityResources.Address());
                        break;
                    case IdentityServerConstants.StandardScopes.Email:
                        identityResources.Add(new IdentityResources.Email());
                        break;
                    case IdentityServerConstants.StandardScopes.Profile:
                        identityResources.Add(new IdentityResources.Profile());
                        break;
                }
            }

            return Task.FromResult<IEnumerable<IdentityResource>>(identityResources);
        }

        public Task<IEnumerable<ApiScope>> FindApiScopesByNameAsync(IEnumerable<string> scopeNames)
        {
            var apiScopes = JoqdsResources.ApiScopes.Where(x => scopeNames.Contains(x.Name)).ToList();
            return Task.FromResult<IEnumerable<ApiScope>>(apiScopes);
        }

        public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            scopeNames = scopeNames.Select(x => x.Split('.')[0]);
            var apiResources = JoqdsResources.ApiResources.Where(x => scopeNames.Contains(x.Name)).ToList();
            return Task.FromResult<IEnumerable<ApiResource>>(apiResources);
        }

        public Task<IEnumerable<ApiResource>> FindApiResourcesByNameAsync(IEnumerable<string> apiResourceNames)
        {
            var apiResources = JoqdsResources.ApiResources.Where(x => apiResourceNames.Contains(x.Name)).ToList();
            return Task.FromResult<IEnumerable<ApiResource>>(apiResources);
        }

        public Task<Resources> GetAllResourcesAsync()
        {
            return Task.FromResult(JoqdsResources);
        }
    }
}