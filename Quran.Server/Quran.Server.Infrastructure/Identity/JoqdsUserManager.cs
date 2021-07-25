using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Quran.Server.Infrastructure.Services;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quran.Server.Infrastructure.Identity
{
    public class JoqdsUserManager : UserManager<ApplicationUser>
    {
        public JoqdsUserManager(IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators, IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }

        public async Task<ApplicationUser> FindByPhoneNumber(string phone, string prefix = "+98")
        {
            if (!UtilitiesService.TryNormalizePhoneNumber(phone, out string normalizedPhone, prefix)) return null;
            return await Users.SingleOrDefaultAsync(x => x.PhoneNumber == normalizedPhone);
        }

        public async Task<ApplicationUser> FindByUsername(string username)
        {
            var user = await Users.SingleOrDefaultAsync(x => String.Equals(x.UserName, username, StringComparison.CurrentCultureIgnoreCase));

            return user;
        }


    }
}