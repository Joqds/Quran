using IdentityModel;

using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Stores;

using Microsoft.Extensions.Configuration;

using Quran.Server.Infrastructure.Identity;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Joqds.Identity.Stores
{
    public class JoqdsClientStore : IClientStore
    {
        private static readonly ClientConfig QuranFlutterMobileClient =
            new ClientConfig(JoqdsConstants.ClientPrefix.QuranFlutterMobile, 1200);

        private static readonly ClientConfig QuranFlutterWebClient =
            new ClientConfig(JoqdsConstants.ClientPrefix.QuranFlutterWeb, 86400);

        private static readonly ClientConfig QuranSwaggerClient =
            new ClientConfig(JoqdsConstants.ClientPrefix.QuranSwagger, 86400);

        private static readonly ClientConfig QuranAdminClient =
            new ClientConfig(JoqdsConstants.ClientPrefix.QuranAdmin, 1200);

        private readonly List<string> _allowedOrigins;

        public JoqdsClientStore(
            IConfiguration configuration)
        {
            _allowedOrigins = configuration.GetSection("AllowedOrigin").Get<List<string>>();
        }

        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            var (clientType, value) = GetClientDetail(clientId);

            switch (clientType)
            {
                case JoqdsClientType.QuranFlutterMobile:
                    return await GetQuranFlutterMobileClient(value, clientId, clientType);
                case JoqdsClientType.QuranFlutterWeb:
                    return await GetQuranFlutterWebClient(value, clientId, clientType);
                case JoqdsClientType.QuranSwagger:
                    return await GetQuranSwaggerClient(value, clientId, clientType);
                case JoqdsClientType.QuranAdmin:
                    return await GetQuranAdminClient(clientId, clientType);
                default:
                    return null;
            }
        }

        public static (JoqdsClientType, Version) GetClientDetail(string clientId)
        {
            var split = clientId.Split('|');
            if ((split[0] != QuranAdminClient.Prefix && split.Length != 2) ||
                (split[0] == QuranAdminClient.Prefix && split.Length != 1))
            {
                return (JoqdsClientType.None, null);
            }

            var joqdsClientType = ClientTypes.GetValueOrDefault(split[0], JoqdsClientType.None);

            switch (joqdsClientType)
            {
                case JoqdsClientType.QuranFlutterMobile:
                case JoqdsClientType.QuranFlutterWeb:
                case JoqdsClientType.QuranSwagger:
                    var valid = Version.TryParse(split[1], out Version version);
                    return valid ? (JoqdsClientType: joqdsClientType, version) : (JoqdsClientType.None, null);
                case JoqdsClientType.QuranAdmin:
                    return (joqdsClientType, null);
                default:
                    return (JoqdsClientType.None, null);
            }
        }

        public enum JoqdsClientType
        {
            None,
            QuranFlutterMobile,
            QuranFlutterWeb,
            QuranSwagger,
            QuranAdmin
        }

        private static readonly Dictionary<string, JoqdsClientType> ClientTypes =
            new Dictionary<string, JoqdsClientType>()
            {
                {QuranFlutterMobileClient.Prefix, JoqdsClientType.QuranFlutterMobile},
                {QuranFlutterWebClient.Prefix, JoqdsClientType.QuranFlutterWeb},
                {QuranSwaggerClient.Prefix, JoqdsClientType.QuranSwagger},
                {QuranAdminClient.Prefix, JoqdsClientType.QuranAdmin}
            };


        private Task<Client> GetQuranAdminClient(string clientId, JoqdsClientType clientType)
        {

            var client = new Client
            {
                ClientId = clientId,
                RequireClientSecret = false,
                ClientName = QuranAdminClient.Prefix,
                AllowOfflineAccess = true,
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                ClientClaimsPrefix = null,
                Enabled = true,
                AlwaysSendClientClaims = true,
                UpdateAccessTokenClaimsOnRefresh = false,
                AccessTokenLifetime = QuranAdminClient.AccessTokenLifetime,
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.OfflineAccess,
                    IdentityServerConstants.StandardScopes.Address,
                    IdentityServerConstants.StandardScopes.Phone,
                    IdentityServerConstants.StandardScopes.Email,
                    JoqdsConstants.ApiResources.QuranAdmin
                },
                AllowedCorsOrigins = _allowedOrigins,
                Claims =
                {
                    new ClientClaim(JoqdsConstants.ClaimTypes.ClientType, clientType.ToString()),
                }
            };
            return Task.FromResult(client);
        }

        private Task<Client> GetQuranFlutterMobileClient(Version version, string clientId, JoqdsClientType clientType)
        {

            var client = new Client
            {
                ClientId = clientId,
                ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },
                AllowOfflineAccess = true,
                AllowedGrantTypes = new[]
                {
                    OidcConstants.GrantTypes.AuthorizationCode,
                },
                AllowedScopes = new[]
                {
                    OidcConstants.StandardScopes.OpenId,
                    OidcConstants.StandardScopes.OfflineAccess,
                    OidcConstants.StandardScopes.Profile,
                    OidcConstants.StandardScopes.Email,
                    OidcConstants.StandardScopes.Phone,
                    JoqdsConstants.ApiResources.QuranApp,
                    JoqdsConstants.ApiResources.Notif,
                    JoqdsConstants.Scope.QuranNotif,
                },
                ClientName = "Joqds Quran App",
                ClientUri = "Quran.Joqds.ir",
                LogoUri = "https://quran.joqds.ir/OUT.png",
                RedirectUris = new[] { "jquran://signin-oidc" },
                FrontChannelLogoutUri = "jquran://signout-oidc",
                PostLogoutRedirectUris = { "jquran://signout-callback-oidc" },
                AllowAccessTokensViaBrowser = true,
                ClientClaimsPrefix = null,
                RefreshTokenUsage = TokenUsage.OneTimeOnly,
                RefreshTokenExpiration = TokenExpiration.Sliding,
                AlwaysSendClientClaims = true,
                AccessTokenLifetime = QuranFlutterMobileClient.AccessTokenLifetime,
                AllowedCorsOrigins = _allowedOrigins,
                Claims =
                {
                    new ClientClaim(JoqdsConstants.ClaimTypes.Version, version.ToString(2)),
                    new ClientClaim(JoqdsConstants.ClaimTypes.ClientType, clientType.ToString()),
                }

            };
            return Task.FromResult(client);

        }

        private Task<Client> GetQuranFlutterWebClient(Version version, string clientId, JoqdsClientType clientType)
        {
            var client = new Client
            {
                ClientId = clientId,
                ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },
                AllowOfflineAccess = true,
                AllowedGrantTypes = new[]
                {
                    OidcConstants.GrantTypes.AuthorizationCode,
                },
                AllowedScopes = new[]
                {
                    OidcConstants.StandardScopes.OpenId,
                    OidcConstants.StandardScopes.OfflineAccess,
                    OidcConstants.StandardScopes.Profile,
                    OidcConstants.StandardScopes.Email,
                    OidcConstants.StandardScopes.Phone,
                    JoqdsConstants.ApiResources.QuranApp,
                    JoqdsConstants.ApiResources.Notif,
                    JoqdsConstants.Scope.QuranNotif,
                },
                ClientName = "Joqds Quran App",
                ClientUri = "Quran.Joqds.ir",
                LogoUri = "https://quran.joqds.ir/OUT.png",
                RedirectUris = new[] { "https://quran.joqds.ir/signin-oidc" },
                FrontChannelLogoutUri = "https://quran.joqds.ir/signout-oidc",
                PostLogoutRedirectUris = { "https://quran.joqds.ir/signout-callback-oidc" },
                AllowAccessTokensViaBrowser = true,
                ClientClaimsPrefix = null,
                RefreshTokenUsage = TokenUsage.OneTimeOnly,
                RefreshTokenExpiration = TokenExpiration.Sliding,
                AlwaysSendClientClaims = true,
                AccessTokenLifetime = QuranFlutterMobileClient.AccessTokenLifetime,
                AllowedCorsOrigins = _allowedOrigins,
                Claims =
                {
                    new ClientClaim(JoqdsConstants.ClaimTypes.Version, version.ToString(2)),
                    new ClientClaim(JoqdsConstants.ClaimTypes.ClientType, clientType.ToString()),
                }

            };
            return Task.FromResult(client);
        }

        private Task<Client> GetQuranSwaggerClient(Version version, string clientId, JoqdsClientType clientType)
        {
            var client = new Client
            {
                ClientId = clientId,
                ClientSecrets = new List<Secret>() { new Secret("3f6ab4da-5dae-404c-ba06-c2ba3686bd94".Sha256(),
                    "Example") },
                RequireClientSecret = true,
                ClientName = QuranSwaggerClient.Prefix,
                AllowedGrantTypes = new []{GrantType.ClientCredentials,GrantType.AuthorizationCode,GrantType.ResourceOwnerPassword,GrantType.DeviceFlow},
                AllowOfflineAccess = true,
                ClientClaimsPrefix = null,
                Enabled = true,
                AlwaysSendClientClaims = true,
                UpdateAccessTokenClaimsOnRefresh = false,
                AccessTokenLifetime = QuranAdminClient.AccessTokenLifetime,
                RequireConsent = true,
                RequirePkce = true,
                UserCodeType = IdentityServerConstants.UserCodeTypes.Numeric,
                UserSsoLifetime = 120,
                AllowedScopes =
                {
                    OidcConstants.StandardScopes.OpenId,
                    OidcConstants.StandardScopes.OfflineAccess,
                    OidcConstants.StandardScopes.Profile,
                    OidcConstants.StandardScopes.Email,
                    OidcConstants.StandardScopes.Phone,
                    JoqdsConstants.ApiResources.QuranApp,
                    JoqdsConstants.ApiResources.Notif,
                    JoqdsConstants.Scope.QuranNotif,
                },
                AllowedCorsOrigins = _allowedOrigins,
                RedirectUris = new List<string>() { "https://quran.api.joqds.ir/swagger/oauth2-redirect.html" },
                Claims =
                {
                    new ClientClaim(JoqdsConstants.ClaimTypes.Version, version.ToString(2)),
                    new ClientClaim(JoqdsConstants.ClaimTypes.ClientType, clientType.ToString()),
                },

            };
            return Task.FromResult(client);
        }

        // ReSharper disable once UnusedMember.Local
        private static string GetClientId(string prefix, string value)
        {
            return $"{prefix}|{value}";
        }
    }
}