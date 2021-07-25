using Quran.Server.Infrastructure.Identity.Helper;

using System;
using System.Linq;

namespace Quran.Server.Infrastructure.Identity.Implementations
{
    public class TotpValidator : ITotpValidator
    {
        private readonly ITotpGenerator _totpGenerator;

        public TotpValidator(ITotpGenerator totpGenerator)
        {
            _totpGenerator = totpGenerator;
        }

        /// <summary>
        /// Validates a given TOTP.
        /// </summary>
        /// <param name="accountSecretKey">User's secret key. Same as used to create the setup.</param>
        /// <param name="clientTotp">Number provided by the user which has to be validated.</param>
        /// <returns>True or False if the validation was successful.</returns>
        public bool Validate(string accountSecretKey, int clientTotp)
        {
            string givenToken = clientTotp.ToString().PadLeft(TotpConstants.TokenLength, '0');
            if (givenToken.Length != TotpConstants.TokenLength)
            {
                return false;
            }
            int timeToleranceInSeconds = TotpConstants.TokenValiditySeconds;
            if (accountSecretKey == null)
            {
                return false;
            }
            var codes = _totpGenerator.GetValidTotps(accountSecretKey, TimeSpan.FromSeconds(timeToleranceInSeconds));
            var codeList = codes.Select(x => x.ToString().PadLeft(TotpConstants.TokenLength, '0')).Select(x => x[^TotpConstants.TokenLength..]).ToList();
            return codeList.Any(c => c == givenToken);
        }
    }
}
