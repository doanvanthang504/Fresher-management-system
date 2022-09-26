using Global.Shared.ViewModels.AttendancesViewModels;
using System;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IAttendanceTokenService
    {
        string GenerateAttendanceTokenURL(GenerateAttendanceTokenViewModel attendanceToken);

        string GenerateAttendanceClassTokenURL(GenerateAttendanceClassTokenViewModel attendanceToken);

        bool VerifyAttendanceToken(string attendaceToken);

        KeyValuePair<Guid, int> GetDataByToken(string attendanceToken);
    }
}
