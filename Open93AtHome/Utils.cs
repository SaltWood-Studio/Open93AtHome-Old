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

        public static ulong[] BlobToUInt64(byte[] blob)
        {
            ulong[] result = new ulong[blob.Length / 8];

            for (int i = 0; i < result.Length; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    result[i] *= 256;
                    result[i] += blob[i * 8 + j];
                }
            }

            return result;
        }

        public static byte[] UInt64ToBlob(ulong[] uint64)
        {
            byte[] result = new byte[uint64.Length * 8];

            for (int i = 0; i < uint64.Length; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    result[i * 8 + (7 - j)] = (byte)(uint64[i] % 256);
                    uint64[i] /= 256;
                }
            }

            return result;
        }
    }
}
