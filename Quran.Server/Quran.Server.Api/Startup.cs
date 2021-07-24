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
using Quran.Server.Infrastructure.Persistence;

namespace Quran.Server.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        readonly string AllowedOrigin = "allowedOrigin";
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
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedHost;
            });
            services.AddApplication();
            services.AddInfrastructure(Configuration);
            services.AddMvc();

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
                option.AddPolicy(AllowedOrigin,
                    builder => builder.AllowCredentials().AllowAnyMethod().AllowAnyHeader()
                        .SetIsOriginAllowed(s => true)
                );
            });
            services.AddOpenApiDocument(settings =>
            {
                settings.Title = "Joqds Quran Api";
                settings.AddSecurity("JWT", Enumerable.Empty<string>(),
                    new OpenApiSecurityScheme()
                    {
                        Type = OpenApiSecuritySchemeType.OAuth2,
                        Flows = new OpenApiOAuthFlows()
                        {
                            AuthorizationCode = new OpenApiOAuthFlow()
                            {
                                AuthorizationUrl = "https://quran.api.joqds.ir/connect/authorize",
                                TokenUrl = "https://quran.api.joqds.ir/connect/token",
                                RefreshUrl = "https://quran.api.joqds.ir/connect/refresh",
                                Scopes = new Dictionary<string, string>
                                {
                                    {"Quran.Server.ApiAPI", "Demo API - full access"},
                                    {OidcConstants.StandardScopes.Profile, "Open Id"},
                                    {OidcConstants.StandardScopes.OpenId, "Open Id"},
                                    { OidcConstants.StandardScopes.OfflineAccess, "Open Id"}
                                },

                            },
                            
                        }
                    });
                settings.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT"));
                settings.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
               
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders();
            IdentityModelEventSource.ShowPII = true;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseDeveloperExceptionPage();
//                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(AllowedOrigin);
            app.UseHealthChecks("/health");
            app.UseOpenApi();

//            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSwaggerUi3();

            app.UseRouting();
            app.UseAuthentication();
            app.UseIdentityServer(new IdentityServerMiddlewareOptions()
            {
                AuthenticationMiddleware = builder => { builder.UseForwardedHeaders(); }
            });
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();

            });
        }
    }
}
