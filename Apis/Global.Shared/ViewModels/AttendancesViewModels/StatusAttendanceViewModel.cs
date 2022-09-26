using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global.Shared.ViewModels.AttendancesViewModels
{
    public class StatusAttendanceViewModel
    {
        public List<StatusAttendanceEnum> AbsentStatuses { 
            get => new List<StatusAttendanceEnum>
            {
                StatusAttendanceEnum.Absent,
                StatusAttendanceEnum.AbsentWithNoPermission
            };  
        }
        public List<StatusAttendanceEnum> LateInEarlyOutStatuses 
        {
            get => new List<StatusAttendanceEnum>
            {
                StatusAttendanceEnum.LateComing,
                StatusAttendanceEnum.LateComingWithNoPermission,
                StatusAttendanceEnum.EarlyLeaving,
                StatusAttendanceEnum.EarlyLeavingWithNoPermission
            };
        }
        public List<StatusAttendanceEnum> NoPermissionStatuses 
        {
            get => new List<StatusAttendanceEnum>
            {
                StatusAttendanceEnum.AbsentWithNoPermission,
                StatusAttendanceEnum.LateComingWithNoPermission,
                StatusAttendanceEnum.EarlyLeavingWithNoPermission
            };
        }
    }
}
