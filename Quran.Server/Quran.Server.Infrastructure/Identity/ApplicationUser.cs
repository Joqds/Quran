using Microsoft.AspNetCore.Identity;

using System;

namespace Quran.Server.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        private string _displayName;

        public string DisplayName
        {
            get => string.IsNullOrWhiteSpace(_displayName) ? UserName : _displayName;
            set => _displayName = value;
        }



    }
}
