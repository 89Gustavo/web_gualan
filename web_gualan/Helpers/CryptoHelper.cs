using System.Security.Cryptography;
using System.Text;

namespace web_gualan.Helpers
{
    public static class CryptoHelper
    {
        private static readonly string key = "1234567890123456";

        public static string Encrypt(string text)
        {
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = new byte[16];

            var encryptor = aes.CreateEncryptor();
            var bytes = Encoding.UTF8.GetBytes(text);

            return Convert.ToBase64String(encryptor.TransformFinalBlock(bytes, 0, bytes.Length));
        }
    }
}
