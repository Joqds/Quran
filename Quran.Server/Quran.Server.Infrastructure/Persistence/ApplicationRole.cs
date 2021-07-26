using Microsoft.AspNetCore.Identity;

using System;

namespace Quran.Server.Infrastructure.Persistence
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole(string roleName) : base(roleName)
        {
        }

        public ApplicationRole()
        {
        }
    }
}