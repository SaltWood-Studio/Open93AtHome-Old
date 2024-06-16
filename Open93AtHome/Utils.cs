using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Open93AtHome
{
    public static class Utils
    {
        public static string GenerateHexString(int length)
        {
            const string hex = "0123456789abcdef";
            Random random = new Random();

            if (length < 0)
            {
                throw new ArgumentException("Length must be a positive integer");
            }

            StringBuilder hexString = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(hex.Length);
                hexString.Append(hex[index]);
            }

            return hexString.ToString();
        }

        public static ulong ByteArrayToUInt64(byte[] blob)
        {
            ulong result = 0;

            for (int j = 0; j < 8; j++)
            {
                result *= 256;
                result += blob[j];
            }

            return result;
        }
    }
}
