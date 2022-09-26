using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global.Shared.ViewModels.AttendancesViewModels
{
    public class GenerateAttendanceTokenViewModel
    {
        public Guid FresherId { get; set; }

        public int ExpiredLinkMinutes { get; set; }

        public int TypeAttendance { get; set; }
    }
}
