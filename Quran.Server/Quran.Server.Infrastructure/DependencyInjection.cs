using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quran.Server.Application.Common.Interfaces;
using Quran.Server.Infrastructure.Identity;
using Quran.Server.Infrastructure.Persistence;
using Quran.Server.Infrastructure.Services;

namespace Quran.Server.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("Joqds.QuranDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddScoped<IDomainEventService, DomainEventService>();

            services
                .AddIdentity<ApplicationUser,ApplicationRole>(options =>
                {
                    options.Lockout.AllowedForNewUsers = false;
                    options.User.RequireUniqueEmail = true;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 1;
                })
                .AddUserManager<UserManager<ApplicationUser>>()
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddRoleManager<RoleManager<ApplicationRole>>()
                .AddDefaultTokenProviders()
                .AddDefaultUI()
                ;

            services.AddIdentityServer(options =>
                {
                    options.PublicOrigin = "https://quran.api.joqds.ir";
                    options.IssuerUri = "https://quran.api.joqds.ir";
                })
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>(options =>
                {
                })
                .AddInMemoryClients(JoqdsClientStore.Clients)
                ;

            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IIdentityService, IdentityService>();

            services.AddAuthentication()
                .AddIdentityServerJwt()
                .AddCookie()
                .AddLocalApi()
                .AddJwtBearer();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator"));
            });

            return services;
        }
    }
}