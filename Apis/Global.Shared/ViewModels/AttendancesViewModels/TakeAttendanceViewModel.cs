using System;

namespace Global.Shared.ViewModels.AttendancesViewModels
{
    public class TakeAttendanceViewModel
    {
        public Guid FresherId { get; set; }

        public long ExpiredTokenTimestamp { get; set; }

        public int TypeAttendance { get; set; }
    }
}
