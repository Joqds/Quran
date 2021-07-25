using System;
using System.Collections.Generic;

namespace Quran.Server.Infrastructure.Identity
{
    public interface ITotpGenerator
    {
        /// <summary>
        /// Generates a valid TOTP.
        /// </summary>
        /// <param name="accountSecretKey">User's secret key. Same as used to create the setup.</param>
        /// <param name="length"></param>
        /// <returns>Creates a 6 digit one time password.</returns>
        int Generate(string accountSecretKey, int? length=null);

        string GenerateString(string accountSecretKey, int? length = null);

        /// <summary>
        /// Gets valid TOTPs.
        /// </summary>
        /// <param name="accountSecretKey">User's secret key. Same as used to create the setup.</param>
        /// <param name="timeTolerance">Time tolerance in seconds to acceppt before and after now.</param>
        /// <returns>List of valid totps.</returns>
        IEnumerable<int> GetValidTotps(string accountSecretKey, TimeSpan timeTolerance);
    }
}
