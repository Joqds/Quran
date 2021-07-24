using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using IdentityModel;

using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace Quran.Server.Infrastructure.Identity
{
    public class JoqdsClientStore : IClientStore
    {
        public static readonly List<Client> Clients = new List<Client>
        {
            new Client
            {
                ClientId = "flutter",
                AccessTokenType = AccessTokenType.Jwt,
                AllowOfflineAccess = true,
                RequireConsent = false,
                AllowedGrantTypes = new[]
                {
                    OidcConstants.GrantTypes.AuthorizationCode,
//                    OidcConstants.GrantTypes.DeviceCode,
//                    OidcConstants.GrantTypes.Implicit,
//                    OidcConstants.GrantTypes.JwtBearer,
//                    OidcConstants.GrantTypes.Password,
//                    OidcConstants.GrantTypes.RefreshToken,
//                    OidcConstants.GrantTypes.Saml2Bearer,
//                    OidcConstants.GrantTypes.TokenExchange
                },
                AllowedScopes = new[]
                {
                    OidcConstants.StandardScopes.OpenId,
                    OidcConstants.StandardScopes.OfflineAccess,
                    OidcConstants.StandardScopes.Profile,
                    "quran", "Quran.Server.ApiAPI"
                },
                ClientName = "Joqds Quran App",
                ClientUri = "Quran.Joqds.ir",
                LogoUri = "https://quran.joqds.ir/OUT.png",
                RedirectUris = new[] {"jquran://oidc"},
                AllowAccessTokensViaBrowser = true,
                RequireClientSecret = false,
            },
            new Client
            {
                ClientId = "swagger",
                ClientName = "Swagger UI for demo_api",
                ClientSecrets = {new Secret("secret".Sha256())}, // change me!

                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = false,
                RequireClientSecret = false,
                AllowOfflineAccess = true,
                RedirectUris = {"https://quran.api.joqds.ir/swagger/oauth2-redirect.html"},
                AllowedCorsOrigins = {"https://quran.api.joqds.ir"},
                AllowedScopes =
                {
                    OidcConstants.StandardScopes.OpenId,
                    OidcConstants.StandardScopes.OfflineAccess,
                    OidcConstants.StandardScopes.Profile,
                    "Quran.Server.ApiAPI",
                },RequireConsent = false
            }
        };


        public Task<Client> FindClientByIdAsync(string clientId)
        {
            return Task.FromResult(Clients.SingleOrDefault(x => x.ClientId == clientId));
        }
    }
}
