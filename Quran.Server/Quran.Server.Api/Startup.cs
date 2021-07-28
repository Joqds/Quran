using FluentValidation.AspNetCore;

using IdentityModel;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;

using NSwag;
using NSwag.AspNetCore;
using NSwag.Generation.Processors.Security;

using Quran.Server.Api.Filters;
using Quran.Server.Api.Services;
using Quran.Server.Application;
using Quran.Server.Application.Common.Interfaces;
using Quran.Server.Infrastructure;
using Quran.Server.Infrastructure.Identity;
using Quran.Server.Infrastructure.Identity.Implementations;
using Quran.Server.Infrastructure.Persistence;

using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Quran.Server.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        readonly string _allowedOrigin = "allowedOrigin";
        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                //options.ForwardLimit = 4;
                //options.KnownProxies.Add(IPAddress.Parse("127.0.10.1"));
                //options.ForwardedForHeaderName = "X-Forwarded-For-My-Custom-Header-Name";
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto |
                                           ForwardedHeaders.XForwardedHost;
            });
            services.AddApplication();
            services.AddInfrastructure(Configuration);
            services.AddMvc();
            services.Configure<IdSeedUsersOptions>(Configuration.GetSection("seed"));
            services.AddSingleton<ITotpGenerator, TotpGenerator>();
            services.AddSingleton<ITotpValidator, TotpValidator>();

            //            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddSingleton<ICurrentUserService, CurrentUserService>();

            services.AddHttpContextAccessor();

            services.AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>();


            services.AddControllers(options => options.Filters.Add<ApiExceptionFilterAttribute>())
                .AddFluentValidation(x => x.AutomaticValidationEnabled = false)
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
                });


            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
            services.AddCors(option =>
            {
                option.AddPolicy(_allowedOrigin,
                    builder => builder.AllowCredentials().AllowAnyMethod().AllowAnyHeader()
                        .SetIsOriginAllowed(s => true)
                );
            });
            services.AddOpenApiDocument(settings =>
            {
                settings.Title = "Joqds Quran Api";
                settings.AddSecurity("Identity of Joqds", Enumerable.Empty<string>(),
                    new OpenApiSecurityScheme()
                    {
                        Type = OpenApiSecuritySchemeType.OpenIdConnect,
                        //                        AuthorizationUrl = "https://id.joqds.ir/connect/authorize",
                        //                        TokenUrl = "https://id.joqds.ir/connect/token",
                        OpenIdConnectUrl = "https://id.joqds.ir/.well-known/openid-configuration",
                        Description = "Login to work",
                        Flow = OpenApiOAuth2Flow.AccessCode,
                        Scopes = new Dictionary<string, string>
                        {
                            {OidcConstants.StandardScopes.Profile, "Open Id"},
                            {OidcConstants.StandardScopes.OpenId, "Open Id"},
                            {OidcConstants.StandardScopes.OfflineAccess, "Open Id"},
                            {OidcConstants.StandardScopes.Email, ""},
                            {OidcConstants.StandardScopes.Phone, ""},
                            {JoqdsConstants.ApiResources.QuranApp, ""},
                            {JoqdsConstants.Scope.QuranNotif, ""},
                        }
                    });
                settings.OperationProcessors.Add(new OperationSecurityScopeProcessor("Identity of Joqds"));
                settings.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("Identity of Joqds"));
                settings.GenerateEnumMappingDescription = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
                IdentityModelEventSource.ShowPII = true;
            }

            app.UseCors(_allowedOrigin);
            app.UseHealthChecks("/health");
            app.UseOpenApi();
            app.UseMigrationsEndPoint(new MigrationsEndPointOptions() { Path = "/mig" });
            //            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSwaggerUi3(settings =>
            {
                settings.OAuth2Client = new OAuth2ClientSettings()
                {
                    ClientId = "QuranSwagger|" + GetType().Assembly.GetName().Version.ToString(2),
                    AppName = "Quran",
                    ClientSecret = "3f6ab4da-5dae-404c-ba06-c2ba3686bd94",
                    UsePkceWithAuthorizationCodeGrant = true
                };
            });

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();

            });
        }
    }
}
