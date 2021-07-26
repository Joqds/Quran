namespace Quran.Server.Infrastructure.Identity
{
    public interface ITotpValidator
    {
        /// <summary>
        /// Validates a given TOTP.
        /// </summary>
        /// <param name="accountSecretKey">User's secret key. Same as used to create the setup.</param>
        /// <param name="clientTotp">Number provided by the user which has to be validated.</param>
        /// <returns>True or False if the validation was successful.</returns>
        bool Validate(string accountSecretKey, int clientTotp);
    }
}
