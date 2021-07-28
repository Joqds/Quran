using Microsoft.AspNetCore.Identity;

using Quran.Server.Infrastructure.Identity;

using System.Linq;
using System.Threading.Tasks;

namespace Quran.Server.Infrastructure.Persistence
{
    public class IdSeedUsersOptions
    {
        public IdUserOptions[] Admins { get; set; }

    }

    public class IdUserOptions
    {
        public string Phone { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager, IdSeedUsersOptions seedDataValue)
        {
            if(seedDataValue?.Admins==null)return;
            var administratorRole = new ApplicationRole(JoqdsConstants.Roles.Admin);
            if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
            {
                await roleManager.CreateAsync(administratorRole);
            }

            foreach (var admin in seedDataValue.Admins)
            {
                //todo: support empty fields
                var administrator = new ApplicationUser()
                {
                    UserName = admin.UserName,
                    Email = admin.Email,
                    EmailConfirmed = true,
                    PhoneNumber = admin.Phone,
                    PhoneNumberConfirmed = true,
                };

                if (!userManager.Users.Any(u => u.UserName != administrator.UserName
                                                || u.Email == admin.Email
                                                || u.PhoneNumber == admin.Phone))
                {
                    await userManager.CreateAsync(administrator, admin.Password);
                    await userManager.AddToRolesAsync(administrator, new[] {administratorRole.Name});
                }
            }

        }

        public static Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            // Seed, if necessary
            return Task.CompletedTask;
        }

    }
}
