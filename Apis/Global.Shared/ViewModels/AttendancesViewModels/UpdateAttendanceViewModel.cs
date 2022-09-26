using Domain.Enums;
using System;

namespace Global.Shared.ViewModels.AttendancesViewModels
{
    public class UpdateAttendanceViewModel
    {
        public Guid AttendanceId { get; set; }
        public StatusAttendanceEnum Status1 { get; set; }

        public StatusAttendanceEnum Status2 { get; set; }

        public string? Note { get; set; }
    }
}
