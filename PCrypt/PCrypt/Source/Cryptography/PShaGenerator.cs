using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PCrypt.Source.Cryptography
{
    class PShaGenerator
    {
        public static string GenerateKey(string pass)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(pass);

            SHA256Managed cipher = new SHA256Managed();
            byte[] encrypted = cipher.ComputeHash(buffer, 0, buffer.Length);

            string temp = BufferToStringKey(encrypted);
            string key = GenerateKeyMD5(temp);
            return key;
        }

        private static string GenerateKeyMD5(string SHA)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(SHA);

            MD5Cng cipher = new MD5Cng();
            byte[] encrypted = cipher.ComputeHash(buffer, 0, buffer.Length);

            return BufferToStringKey(encrypted);
        }

        private static string BufferToStringKey(byte[] buffer)
        {
            string key = string.Empty;
            foreach (byte b in buffer)
            {
                key += b.ToString("x2");
            }

            return key;
        }
    }
}
