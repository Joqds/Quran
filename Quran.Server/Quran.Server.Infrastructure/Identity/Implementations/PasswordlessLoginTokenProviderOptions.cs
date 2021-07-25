using Microsoft.AspNetCore.Identity;
using Quran.Server.Infrastructure.Identity.Helper;

namespace Quran.Server.Infrastructure.Identity.Implementations
{
    public class PasswordlessLoginTokenProviderOptions : DataProtectionTokenProviderOptions
    {
        public PasswordlessLoginTokenProviderOptions()
        {
            Name = TotpConstants.TokenProviderName;
            TokenLifespan = System.TimeSpan.FromMinutes(15);
        }
    }
}
