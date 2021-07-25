
using System.Text.RegularExpressions;

namespace Quran.Server.Infrastructure.Services
{
    public static class UtilitiesService
    {
        public static string NormalizePhoneNumber(string phone, string prefix = "98")
        {
            if (!int.TryParse(phone, out int phoneInt)) return null;

            var result = phoneInt.ToString("+############");
            if (result.Length == 11) result = result.Replace("+", "+" + prefix);
            if (result.Length == 13) return result;
            return null;
        }

        public static bool TryNormalizePhoneNumber(string phone, out string normalizedPhone, string prefix = "+98")
        {
            normalizedPhone = NormalizePhoneNumber(phone, prefix);
            return string.IsNullOrEmpty(phone);

        }
    }
}