using System;
using System.Collections.Generic;
using Quran.Server.Infrastructure.Identity.Helper;

namespace Quran.Server.Infrastructure.Identity.Implementations
{
    public class TotpGenerator : ITotpGenerator
    {
        private readonly DateTime _unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Generates a valid TOTP.
        /// </summary>
        /// <param name="accountSecretKey">User's secret key. Same as used to create the setup.</param>
        /// <param name="length"></param>
        public int Generate(string accountSecretKey,int? length=null)
        {
            var iteration = GetCurrentCounter();
            return TotpHasher.Hash(accountSecretKey, iteration, length??TotpConstants.TokenLength);
        }

        public string GenerateString(string accountSecretKey, int? length = null)
        {
            return Generate(accountSecretKey, length)
                .ToString()
                .PadLeft(length ?? TotpConstants.TokenLength, '0');
        }

        /// <summary>
        /// Gets valid TOTPs.
        /// </summary>
        /// <param name="accountSecretKey">User's secret key. Same as used to create the setup.</param>
        /// <param name="timeTolerance">Time tolerance in seconds to acceppt before and after now.</param>
        /// <returns>List of valid totps.</returns>
        public IEnumerable<int> GetValidTotps(string accountSecretKey, TimeSpan timeTolerance)
        {
            var codes = new List<int>();
            var iterationCounter = GetCurrentCounter();
            var iterationOffset = 0;

            if (timeTolerance.TotalSeconds > TotpConstants.TokenLifeSeconds)
            {
                iterationOffset = Convert.ToInt32(timeTolerance.TotalSeconds / TotpConstants.TokenLifeSeconds);
            }

            var iterationStart = iterationCounter - iterationOffset;
            var iterationEnd = iterationCounter + iterationOffset;

            for (var counter = iterationStart; counter <= iterationEnd; counter++)
            {
                codes.Add(Generate(accountSecretKey, counter,6));
            }

            return codes.ToArray();
        }

        #region Helpers

        private int Generate(string accountSecretKey, long counter, int digits = TotpConstants.TokenLength)
        {
            return TotpHasher.Hash(accountSecretKey, counter, digits);
        }

        private long GetCurrentCounter()
        {
            return (long)(DateTime.UtcNow - _unixEpoch).TotalSeconds / TotpConstants.TokenLifeSeconds;
        }

        #endregion
    }
}
