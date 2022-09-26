using System.Text.RegularExpressions;

namespace Global.Shared.Extensions
{
    public static class StringExtensions
    {
        public static bool IsInDateFormat(this string value)
        {
            return new Regex("$\\d{2}/\\d{2}/\\d{4}^").IsMatch(value);
        }
    }
}
