using IdentityServer4.EntityFramework.Storage;
using IdentityServer4.Services;

using Joqds.Identity.Stores;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;

using Quran.Server.Infrastructure.Identity;
using Quran.Server.Infrastructure.Identity.Implementations;
using Quran.Server.Infrastructure.Persistence;

namespace Joqds.Identity
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }
        readonly string _allowedOrigin = "allowedOrigin";

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

            services.AddCors(option =>
            {
                option.AddPolicy(_allowedOrigin,
                    policyBuilder => policyBuilder.AllowCredentials().AllowAnyMethod().AllowAnyHeader()
                        .SetIsOriginAllowed(s => true)
                );
            });

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddHttpClient();
            services.AddHttpContextAccessor();

            services.AddSingleton<ITotpGenerator, TotpGenerator>();
            services.AddSingleton<ITotpValidator, TotpValidator>();
            services.AddTransient<ICorsPolicyService, JoqdsCorsPolicyService>();
            services.AddTransient<IUserClaimsPrincipalFactory<ApplicationUser>, JoqdsUserClaimsPrincipalFactory>();
            services.AddScoped<JoqdsClientStore>();
            services.AddScoped<JoqdsResourceStore>();

            if (Configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("Joqds.QuranDb"));

                services.AddOperationalDbContext(options =>
                    options.ConfigureDbContext = db =>
                        db.UseInMemoryDatabase("IdentityConfiguration"));

                services.AddConfigurationDbContext(options =>
                    options.ConfigureDbContext = db =>
                        db.UseInMemoryDatabase("IdentityConfiguration"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        Configuration.GetConnectionString("QuranDb"),
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

                services.AddOperationalDbContext(options =>
                    options.ConfigureDbContext = db =>
                        db.UseSqlServer(Configuration.GetConnectionString("IdentityOperational"),
                            sql => sql.MigrationsAssembly(GetType().Assembly.FullName)));

                services.AddConfigurationDbContext(options =>
                    options.ConfigureDbContext = db =>
                        db.UseSqlServer(Configuration.GetConnectionString("IdentityConfiguration"),
                            sql => sql.MigrationsAssembly(GetType().Assembly.FullName)));
            }


            var builder = services.AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;

                    // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
                    options.EmitStaticAudienceClaim = true;
                })
                .AddClientStore<JoqdsClientStorePersist>()
                .AddResourceStore<JoqdsResourceStorePersist>()
                .AddAspNetIdentity<ApplicationUser>()
                .AddProfileService<JoqdsProfileService>()
                .AddExtensionGrantValidator<TotpGrantValidator>()
                .AddCorsPolicyService<JoqdsCorsPolicyService>()
                .AddResourceOwnerValidator<JoqdsResourceOwnerValidator>()
                ;


            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddClaimsPrincipalFactory<JoqdsUserClaimsPrincipalFactory>()
                .AddSignInManager<JoqdsSignInManager>()
                .AddRoleManager<JoqdsRoleManager>()
                .AddDefaultTokenProviders();

            // not recommended for production - you need to store your key material somewhere secure
            builder.AddDeveloperSigningCredential();

            services.AddAuthentication();

            services.AddScoped<JoqdsSignInManager>();
            services.AddScoped<JoqdsUserManager>();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policyBuilder =>
                {
                    policyBuilder.WithOrigins();
                    policyBuilder.AllowAnyHeader();
                    policyBuilder.AllowAnyMethod();
                });
            });
            //todo: implement this
            //            services.AddAuthentication()
            //                .AddGoogle(options =>
            //                {
            //                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
            //
            //                    // register your IdentityServer with Google at https://console.developers.google.com
            //                    // enable the Google+ API
            //                    // set the redirect URI to https://localhost:5001/signin-google
            //                    options.ClientId = "copy client ID from Google here";
            //                    options.ClientSecret = "copy client secret from Google here";
            //                });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseForwardedHeaders();
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                IdentityModelEventSource.ShowPII = true;
            }

            app.UseStaticFiles();

            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();

            });
        }
    }

}
