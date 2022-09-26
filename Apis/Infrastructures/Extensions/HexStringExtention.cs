using System;
using System.Text;

namespace Infrastructures.Extensions
{
    public static class HexStringExtention
    {     
        public static string ToHexString(this byte[] byteArray)
        {
            StringBuilder result = new();
            foreach (byte b in byteArray)
            {
                result.AppendFormat("{0:x2}", b);
            }
            return result.ToString();
        }

        public static string FromHexString(this string hexString)
        {
            var bytes = new byte[hexString.Length / 2];
            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }
            return Encoding.ASCII.GetString(bytes);
        }
    }
}
