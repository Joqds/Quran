using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Quran.Server.Infrastructure.Identity;
using Quran.Server.Infrastructure.Persistence;

using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Quran.Server.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();

                    if (context.Database.IsSqlServer())
                    {
                        context.Database.Migrate();

                        context.SeedQuranData();
                    }

                    var seedData = services.GetService<IOptions<IdSeedUsersOptions>>();
                    if (seedData?.Value != null)
                    {
                        var userManager = services.GetRequiredService<JoqdsUserManager>();
                        var roleManager = services.GetRequiredService<JoqdsRoleManager>();

                        await ApplicationDbContextSeed.SeedDefaultUserAsync(userManager, roleManager,seedData.Value);

                    }

                    await ApplicationDbContextSeed.SeedSampleDataAsync(context);
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                    logger.LogError(ex, "An error occurred while migrating or seeding the database.");

                    throw;
                }
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls("http://0.0.0.0:5002/", "https://0.0.0.0:5003/");
                    webBuilder.UseStartup<Startup>();
                });
    }
}
