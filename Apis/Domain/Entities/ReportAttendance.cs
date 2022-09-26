using System;

namespace Domain.Entities
{
    public class ReportAttendance : BaseEntity
    {
        public Guid FresherId { get; set; }

        public int NumberOfAbsent { get; set; }

        public int NumberOfLateInEarlyOut { get; set; }

        public int MonthAttendance { get; set; }

        public int YearAttendance { get; set; }

        public double NoPermissionRate { get; set; }

        public double DisciplinaryPoint { get; set; }
    }
}
