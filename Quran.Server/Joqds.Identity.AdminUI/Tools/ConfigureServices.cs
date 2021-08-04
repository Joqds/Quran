using Joqds.Identity.AdminUI.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Joqds.Identity.AdminUI.Tools
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddAdminUiServices(this IServiceCollection services)
        {
            services.AddScoped<ClientService>();
            services.AddCoreAdmin(ConstSettings.RoleRequire);
            return services;
        }
    }
}
