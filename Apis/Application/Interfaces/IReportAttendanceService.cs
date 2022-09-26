using Global.Shared.ViewModels.AttendancesViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IReportAttendanceService
    {
        Task<List<ReportAttendanceViewModel>> GetAllReportAttendanceAsync();
        Task<List<ReportAttendanceViewModel>> CreateReportAttendanceByClass(CreateReportAttendanceViewModel request);
    }
}
