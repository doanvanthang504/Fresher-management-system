using System;
using System.Collections.Generic;

namespace Global.Shared.ViewModels
{
    public class FresherAttendancesViewModel
    {
        public Guid Id { get; set; }
        public string AccountName { get; set; }
        public string ClassCode { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string? RECer { get; set; }

        public ICollection<AttendanceViewModel> Attendances { get; set; }
    }
}
