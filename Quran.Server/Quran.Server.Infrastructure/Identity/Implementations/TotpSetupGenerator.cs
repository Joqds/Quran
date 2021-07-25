using System;
using IdentityServer4.Extensions;
using Quran.Server.Infrastructure.Identity.Helper;
using Quran.Server.Infrastructure.Identity.Model;

namespace Quran.Server.Infrastructure.Identity.Implementations
{
    public class TotpSetupGenerator : ITotpSetupGenerator
    {

        /// <summary>
        /// Generates an object you will need so that the user can setup his Google Authenticator to be used with your app.
        /// </summary>
        /// <param name="issuer">Your app name or company for example.</param>
        /// <param name="accountIdentity">Name, Email or Id of the user, without spaces, this will be shown in google authenticator.</param>
        /// <param name="accountSecretKey">A secret key which will be used to generate one time passwords. This key is the same needed for validating a passed TOTP.</param>
        /// <param name="qrCodeWidth">Height of the QR code. Default is 300px.</param>
        /// <param name="qrCodeHeight">Width of the QR code. Default is 300px.</param>
        /// <param name="useHttps">Use Https on google api or not.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns>TotpSetup with ManualSetupKey and QrCode.</returns>
        public TotpSetup Generate(
            string issuer,
            string accountIdentity,
            string accountSecretKey,
            int qrCodeWidth = TotpConstants.QrCodeWidth,
            int qrCodeHeight = TotpConstants.QrCodeHeight,
            bool useHttps = true
        )
        {
            if(issuer.IsNullOrEmpty())throw new ArgumentNullException(nameof(issuer));
            if(accountIdentity.IsNullOrEmpty())throw new ArgumentNullException(nameof(accountIdentity));
            if(accountSecretKey.IsNullOrEmpty())throw new ArgumentNullException(nameof(accountSecretKey));

            accountIdentity = accountIdentity.Replace(" ", "");
            var encodedSecretKey = Base32.Encode(accountSecretKey);
            var provisionUrl =
                $"otpauth://totp/{accountIdentity}?secret={encodedSecretKey}&issuer={UrlEncoder.Encode(issuer)}";
            // var protocol = useHttps ? "https" : "http";
            // var url = $"{protocol}://chart.googleapis.com/chart?cht=qr&chs={qrCodeWidth}x{qrCodeHeight}&chl={provisionUrl}";

            var totpSetup = new TotpSetup
            {
                QrCodeImageUrl = GetQrImage(provisionUrl,qrCodeWidth,qrCodeHeight),
                ManualSetupKey = encodedSecretKey
            };

            return totpSetup;
        }

        private string GetQrImage(string url,int qrCodeWidth,int qrCodeHeight)
        {
            //todo: change it  its deprecated
            var qrUrl = $"https://chart.googleapis.com/chart?cht=qr&chs={qrCodeWidth}x{qrCodeHeight}&chl={url}";
            return qrUrl;
//            return "data:image/png;base64," + Convert.ToBase64String(_utilitiesService.GetQrCode(url,QRCodeGenerator.ECCLevel.H));
        }
    }
}
