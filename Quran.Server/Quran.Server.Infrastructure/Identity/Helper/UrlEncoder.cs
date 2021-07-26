using System.Text;

namespace Quran.Server.Infrastructure.Identity.Helper
{
    internal static class UrlEncoder
    {
        private const string ValidChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";

        internal static string Encode(string value)
        {
            var result = new StringBuilder();

            foreach (var symbol in value)
            {
                if (ValidChars.IndexOf(symbol) != -1)
                {
                    result.Append(symbol);
                }
                else
                {
                    result.Append('%').AppendFormat("{0:X2}", (int)symbol);
                }
            }

            return result.ToString().Replace(" ", "%20");
        }
    }
}
