namespace Quran.Server.Infrastructure.Identity.Helper
{
    public static class TotpConstants
    {
        public const int TokenLifeSeconds = 30;
        public const int CountDownSeconds = TokenLifeSeconds;

        public const int TokenValiditySeconds = TokenLifeSeconds * 4;

        public const int TokenLength = 4;

        public const int QrCodeHeight = 300;

        public const int QrCodeWidth = 300;

        public const string TokenProviderName = "PasswordlessLoginTotpProvider";

        public const string TokenNumericError = "Token must be numeric";


    }
}
