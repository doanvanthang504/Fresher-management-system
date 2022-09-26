using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global.Shared.ViewModels.AttendancesViewModels
{
    public class CreateReportAttendanceViewModel
    {
        public Guid ClassId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
