using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global.Shared.ViewModels.AttendancesViewModels
{
    public class ReportAttedanceStatisticViewModel
    {
        public Guid FresherId { get; set; }
        public int NumberOfAbsent { get; set; }
        public int NumberOfLateInEarlyOut { get; set; }
        public int NumberOfNoPermission { get; set; }
        public int NumberOfAbsent2 { get; set; }
        public int NumberOfLateInEarlyOut2 { get; set; }
        public int NumberOfNoPermission2 { get; set; }
        public int NumberOfAttendanceCheckTimes { get; set; }
        public int MonthAttendance { get; set; }
        public int YearAttendance { get; set; }
    }
}
