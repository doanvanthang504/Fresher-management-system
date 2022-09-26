using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global.Shared.ViewModels.AttendancesViewModels
{
    public class ReportAttendanceViewModel
    {
        public Guid Id { get; set; }
        public Guid FresherId { get; set; }
        public int NumberOfAbsent { get; set; }
        public int NumberOfLateInEarlyOut { get; set; }
        public int MonthAttendance { get; set; }
        public int YearAttendance { get; set; }
        public double NoPermissionRate { get; set; }
        public double DisciplinaryPoint { get; set; }
    }
}
