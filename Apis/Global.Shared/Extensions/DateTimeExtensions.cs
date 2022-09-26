using System;

namespace Global.Shared.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateOnly ToDateOnly(this DateTime dateTime)
        {
            return DateOnly.FromDateTime(dateTime);
        }

        public static DateTimeOffset ToDateTimeOffset(this DateOnly dateOnly)
        {
            return new DateTimeOffset(dateOnly.ToDateTime());
        }

        public static DateTime ToDateTime(this DateOnly dateOnly)
        {
            return dateOnly.ToDateTime(TimeOnly.MinValue);
        }

        public static DateOnly Today()
        {
            return DateTimeOffset.UtcNow.DateTime.ToDateOnly();
        }
    }
}
