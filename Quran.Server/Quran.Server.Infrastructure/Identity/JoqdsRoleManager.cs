using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

using Quran.Server.Infrastructure.Persistence;

using System.Collections.Generic;

namespace Quran.Server.Infrastructure.Identity
{
    public class JoqdsRoleManager : RoleManager<ApplicationRole>
    {
        public JoqdsRoleManager(IRoleStore<ApplicationRole> store, IEnumerable<IRoleValidator<ApplicationRole>> roleValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, ILogger<RoleManager<ApplicationRole>> logger) : base(store, roleValidators, keyNormalizer, errors, logger)
        {
        }
    }
}