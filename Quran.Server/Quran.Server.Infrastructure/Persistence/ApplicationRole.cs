using System;
using Microsoft.AspNetCore.Identity;

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