using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructures.Extensions
{
    public static class CryptographyExtention
    {
        public static string HmacSha256Encode(string payload, string secretKey)
        {
            byte[] keyEncode = Encoding.ASCII.GetBytes(secretKey);
            using var hmacsha256 = new HMACSHA256(keyEncode);
            byte[] byteArray = Encoding.ASCII.GetBytes(payload);
            using var stream = new MemoryStream(byteArray);
            return hmacsha256.ComputeHash(stream).ToHexString();
        }
    }
}
