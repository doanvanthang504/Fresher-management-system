using Global.Shared.Commons;
using Global.Shared.Extensions;
using System;
using System.Globalization;

namespace Global.Shared.Helpers
{
    public static class DateTimeHelper
    {
        private static readonly string _dateFormat = Constant.DATE_TIME_FORMAT_MMddyyyy;
        private static readonly CultureInfo _cultureInfo = CultureInfo.InvariantCulture;
        private static readonly int _month = DateTime.UtcNow.Month;
        private static readonly int _year = DateTime.UtcNow.Year;
        private static readonly DateTime _currentDate = DateTime.UtcNow;

        public static DateOnly GetStartDateOfMonth()
            => DateTime.ParseExact($"{_month:00}/01/{_year}",
                                   _dateFormat,
                                   _cultureInfo).ToDateOnly();

        public static DateOnly GetEndDateOfMonth()
            => DateTime.ParseExact($"{_month:00}/{DateTime.DaysInMonth(_year, _month)}/{_year}",
                                   _dateFormat,
                                   _cultureInfo).ToDateOnly();

        public static DateOnly GetMonday()
            => _currentDate.AddDays(
                _currentDate.Day - (int)_currentDate.DayOfWeek + 1 - _currentDate.Day).ToDateOnly();

        public static DateOnly GetSunday()
            => _currentDate.AddDays(
                _currentDate.Day - (int)_currentDate.DayOfWeek + 7 - _currentDate.Day).ToDateOnly();
    }
}
