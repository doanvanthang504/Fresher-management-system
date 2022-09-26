using Domain.Enums;
using System;

namespace Global.Shared.ViewModels
{
    public class AttendanceViewModel
    {
        public Guid Id { get; set; }

        public Guid FresherId { get; set; }

        public StatusAttendanceEnum Status1 { get; set; }

        public bool IsAttend1 { get; set; } = false;

        public DateTimeOffset AttendDate1 { get; set; }

        public StatusAttendanceEnum Status2 { get; set; }

        public DateTimeOffset AttendDate2 { get; set; }

        public bool IsAttend2 { get; set; } = false;

        public string? Note { get; set; }
    }
}
