using Domain.Enums;
using System;

namespace Domain.Entities
{
    public class Attendance : BaseEntity
    {
        public Guid FresherId { get; set; }

        public Fresher? Fresher { get; set; }

        public StatusAttendanceEnum Status1 { get; set; } = StatusAttendanceEnum.AbsentWithNoPermission;

        public bool IsAttend1 { get; set; } = false;

        public DateTimeOffset AttendDate1 { get; set; }

        public StatusAttendanceEnum Status2 { get; set; } = StatusAttendanceEnum.AbsentWithNoPermission;    

        public DateTimeOffset AttendDate2 { get; set; }

        public bool IsAttend2 { get; set; } = false;

        public string? Note { get; set; }
    }
}
