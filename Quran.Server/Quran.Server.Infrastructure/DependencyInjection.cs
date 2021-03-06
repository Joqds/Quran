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
                        configuration.GetConnectionString("QuranDb"),
                        b =>
                        {
                            b.MigrationsHistoryTable("__EFMigrationsHistory");
                            b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                        }));
            }
            services.AddIdentity();
            services.AddJwt(configuration);
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddScoped<IDomainEventService, DomainEventService>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator"));
            });

            return services;
        }
    }
}