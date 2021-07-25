using System;
using System.Collections.Generic;
using System.Text;

namespace Quran.Server.Infrastructure.Identity.Helper
{
    internal static class Base32
    {
        private const string AllowedCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";

        internal static string Encode(string accountSecretKey)
        {
            var data = Encoding.UTF8.GetBytes(accountSecretKey);
            var output = "";

            for (var bitIndex = 0; bitIndex < data.Length * 8; bitIndex += 5)
            {
                var dualbyte = data[bitIndex / 8] << 8;
                if ((bitIndex / 8) + 1 < data.Length)
                    dualbyte |= data[(bitIndex / 8) + 1];

                dualbyte = 0x1f & (dualbyte >> (16 - (bitIndex % 8) - 5));
                output += AllowedCharacters[dualbyte];
            }

            return output;
        }

        internal static string Decode(string base32)
        {
            var output = new List<byte>();
            var bytes = base32.ToCharArray();

            for (var bitIndex = 0; bitIndex < base32.Length * 5; bitIndex += 8)
            {
                var dualbyte = AllowedCharacters.IndexOf(bytes[bitIndex / 5]) << 10;

                if ((bitIndex / 5) + 1 < bytes.Length)
                    dualbyte |= AllowedCharacters.IndexOf(bytes[(bitIndex / 5) + 1]) << 5;

                if ((bitIndex / 5) + 2 < bytes.Length)
                    dualbyte |= AllowedCharacters.IndexOf(bytes[(bitIndex / 5) + 2]);

                dualbyte = 0xff & (dualbyte >> (15 - (bitIndex % 5) - 8));
                output.Add((byte)(dualbyte));
            }

            var key = Encoding.UTF8.GetString(output.ToArray());
            if (key.EndsWith("\0"))
            {
                var index = key.IndexOf("\0", StringComparison.Ordinal);
                key = key.Remove(index, 1);
            }

            return key;
        }
    }
}
