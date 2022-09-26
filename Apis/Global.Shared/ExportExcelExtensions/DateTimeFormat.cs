using System;

namespace Global.Shared.ExportExcelExtensions
{
    public static class DateTimeFormat
    {
        public static string ToShortDate(this string values)
        {
            var outPut = DateTime.TryParse(values, out var date);
            if (outPut)
            {
                return date.ToString("dd-MMM-yy");
            }
            return "";
        }

        public static string ToShortDate(this DateTime values)
        {
            return values.ToString("dd-MMM-yy");
        }
    }
}
