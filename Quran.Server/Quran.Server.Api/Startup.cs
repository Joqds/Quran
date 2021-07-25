using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using FluentValidation.AspNetCore;
using IdentityModel;
using Joqds.Identity.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.IdentityModel.Logging;
using NSwag;
using NSwag.Generation.Processors.Security;
using Quran.Server.Api.Filters;
using Quran.Server.Api.Services;
using Quran.Server.Application;
using Quran.Server.Application.Common.Interfaces;
using Quran.Server.Infrastructure;
using Quran.Server.Infrastructure.Identity;
using Quran.Server.Infrastructure.Identity.Implementations;
using Quran.Server.Infrastructure.Persistence;

namespace Quran.Server.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        readonly string _allowedOrigin = "allowedOrigin";
        public IConfiguration Configuration { get; }

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

            services.AddSingleton<ITotpGenerator, TotpGenerator>();
            services.AddSingleton<ITotpValidator, TotpValidator>();

            //            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddSingleton<ICurrentUserService, CurrentUserService>();

            services.AddHttpContextAccessor();

            services.AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>();


            services.AddControllers(options => options.Filters.Add<ApiExceptionFilterAttribute>())
                .AddFluentValidation(x => x.AutomaticValidationEnabled = false);


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
                        Type = OpenApiSecuritySchemeType.OAuth2,
                        AuthorizationUrl = "https://id.joqds.ir/connect/authorize",
                        TokenUrl = "https://id.joqds.ir/connect/token",
                        OpenIdConnectUrl = "https://id.joqds.ir/connect",
                        Flows = new OpenApiOAuthFlows()
                        {
                            AuthorizationCode = new OpenApiOAuthFlow()
                            {
                                AuthorizationUrl = "https://id.joqds.ir/connect/authorize",
                                TokenUrl = "https://id.joqds.ir/connect/token",
                                RefreshUrl = "https://id.joqds.ir/connect/refresh",
                                Scopes = new Dictionary<string, string>
                                {
                                    {OidcConstants.StandardScopes.Profile, "Open Id"},
                                    {OidcConstants.StandardScopes.OpenId, "Open Id"},
                                    {OidcConstants.StandardScopes.OfflineAccess, "Open Id"},
                                    {OidcConstants.StandardScopes.Email, ""},
                                    {OidcConstants.StandardScopes.Phone, ""},
                                    {JoqdsConstants.ApiResources.QuranApp, ""},
                                    {JoqdsConstants.Scope.QuranNotif, ""},
                                },

                            }
                            //                            Password = new OpenApiOAuthFlow()
                            //                            {
                            //                                RefreshUrl = "https://id.joqds.ir/connect/refresh",
                            //                                AuthorizationUrl = "https://id.joqds.ir/connect/authorize",
                            //                                TokenUrl = "https://id.joqds.ir/connect/token",
                            //                                Scopes = {{OidcConstants.StandardScopes.Profile, "Open Id"},
                            //                                    {OidcConstants.StandardScopes.OpenId, "Open Id"},
                            //                                    {OidcConstants.StandardScopes.OfflineAccess, "Open Id"},
                            //                                    {OidcConstants.StandardScopes.Email, ""},
                            //                                    {OidcConstants.StandardScopes.Phone, ""},
                            //                                    {JoqdsConstants.ApiResources.QuranApp, ""},
                            //                                    {JoqdsConstants.Scope.QuranNotif, ""},
                            //
                            //                                }
                            //                            },
                        }
                    });
                settings.OperationProcessors.Add(new OperationSecurityScopeProcessor("Identity of Joqds"));
                settings.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("Identity of Joqds"));

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
            app.UseMigrationsEndPoint(new MigrationsEndPointOptions() {Path = "/mig"});
            //            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSwaggerUi3();

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
