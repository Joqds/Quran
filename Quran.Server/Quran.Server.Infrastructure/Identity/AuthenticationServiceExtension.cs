using System;
using Joqds.Identity.Tools;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IdentityModel.AspNetCore.AccessTokenValidation;
using Microsoft.AspNetCore.Identity;
using Quran.Server.Infrastructure.Identity.Helper;
using Quran.Server.Infrastructure.Identity.Implementations;
using Quran.Server.Infrastructure.Persistence;
using TokenOptions = Joqds.Identity.Tools.TokenOptions;

namespace Quran.Server.Infrastructure.Identity
{
    public static class IdentityServiceExtension
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddIdentityCore<ApplicationUser>(idOptions =>
                {
                    idOptions.Password.RequireDigit = false;
                    idOptions.Password.RequiredLength = 6;
                    idOptions.Password.RequiredUniqueChars = 1;
                    idOptions.Password.RequireLowercase = false;
                    idOptions.Password.RequireNonAlphanumeric = false;
                    idOptions.Password.RequireUppercase = false;

                    idOptions.SignIn.RequireConfirmedAccount = false;
                    idOptions.SignIn.RequireConfirmedEmail = false;
                    idOptions.SignIn.RequireConfirmedPhoneNumber = true;

                    idOptions.User.RequireUniqueEmail = false;
                })
                .AddDefaultTokenProviders()
                .AddSignInManager<JoqdsSignInManager>()
                .AddUserManager<JoqdsUserManager>()
                .AddRoles<ApplicationRole>()
                .AddRoleManager<JoqdsRoleManager>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddPasswordlessLoginTotpTokenProvider();

            return services;
        }
    }

    public static class IdentityBuilderExtensions
    {
        public static IdentityBuilder AddPasswordlessLoginTotpTokenProvider(this IdentityBuilder builder)
        {
            var userType = builder.UserType;
            var totpProvider = typeof(PasswordlessLoginTotpTokenProvider);
            return builder.AddTokenProvider(TotpConstants.TokenProviderName, totpProvider);
        }
    }

    public static class AuthenticationServiceExtension
    {
        public static IServiceCollection AddJwt(this IServiceCollection services, IConfiguration config)
        {
            var tokenOptions = config.GetSection("Jwt").Get<TokenOptions>();

            services
                .AddAuthentication(authOptions =>
                {
                    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(jwtOptions =>
                {
                    jwtOptions.TokenValidationParameters.RoleClaimType = JoqdsConstants.ClaimTypes.Role;
                    jwtOptions.Authority = tokenOptions.Authority;
                    jwtOptions.Audience = tokenOptions.Audience;
                    jwtOptions.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
                    jwtOptions.ForwardDefaultSelector = Selector.ForwardReferenceToken("introspection");
                    jwtOptions.RequireHttpsMetadata = false;

//                    jwtOptions.MetadataAddress = ;
                    jwtOptions.SaveToken = true;
                })
                .AddOAuth2Introspection("introspection", options =>
                {
                    options.RoleClaimType = JoqdsConstants.ClaimTypes.Role;
                    options.Authority = tokenOptions.Authority;
                    options.ClientId = tokenOptions.Audience;
                    options.ClientSecret = tokenOptions.Key;
                    options.DiscoveryPolicy.RequireHttps = false;
                });
            services.AddScopeTransformation();
            services.AddAuthorization(options =>
            {

                //example
                options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator"));


            });

            return services;
        }
    }
}