using Global.Shared.ViewModels;
using Global.Shared.ViewModels.AttendancesViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAttendanceService
    {
        Task<bool> SendAttendanceEmailToFresherAsync(GenerateAttendanceClassTokenViewModel attendanceClassToken);

        Task<bool> TakeAttendanceAsync(string token);

        Task<AttendanceViewModel?> UpdateAttendanceAsync(UpdateAttendanceViewModel attendance);

        Task<List<AttendanceViewModel>> GetAllAttendanceByFresherIdAsync(FilterAttendanceViewModel attendance);

        Task<List<FresherAttendancesViewModel>> GetAllAttendanceByClassIdAsync(FilterAttendanceViewModel attendance);

        Task<string> GenenerateLinkAttendanceAsync(GenerateAttendanceClassTokenViewModel attendanceToken);

        Task<bool> TakeAttendanceClassAsync(string token, string tokenUser);

        bool IsValidAttendanceToken(string token);
    }
}
