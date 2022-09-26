using Application.Interfaces;
using Global.Shared.Commons;
using Global.Shared.Exceptions;
using Global.Shared.ViewModels.AttendancesViewModels;
using Infrastructures.Extensions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class AttendanceTokenService : IAttendanceTokenService
    {
        private readonly IConfiguration _configuration;
        private readonly ICurrentTime _currentTime;

        public AttendanceTokenService(IConfiguration configuration, ICurrentTime currentTime)
        {
            _configuration = configuration;
            _currentTime = currentTime;
        }

        public string GenerateAttendanceTokenURL(GenerateAttendanceTokenViewModel attendanceToken)
        {
            var expiredTokenTimestamp = new DateTimeOffset(_currentTime.GetCurrentTime().AddMinutes(attendanceToken.ExpiredLinkMinutes)).ToUnixTimeSeconds();
            var payload = attendanceToken.FresherId + "." + expiredTokenTimestamp + "." + attendanceToken.TypeAttendance;
            var secretKey = _configuration["Attendance:Key"];
            var hmasha256Token = CryptographyExtention.HmacSha256Encode(payload, secretKey);
            var payloadByteArray = Encoding.ASCII.GetBytes(payload);
            var payloadToken = payloadByteArray.ToHexString();
            var url = _configuration["Attendance:Url"];
            return url + payloadToken + hmasha256Token;
        }

        public string GenerateAttendanceClassTokenURL(GenerateAttendanceClassTokenViewModel attendanceToken)
        {
            var expiredTokenTimestamp = new DateTimeOffset(_currentTime.GetCurrentTime().AddMinutes(attendanceToken.ExpiredLinkMinutes)).ToUnixTimeSeconds();
            var payload = attendanceToken.ClassId + "." + expiredTokenTimestamp + "." + attendanceToken.TypeAttendance;
            var secretKey = _configuration["Attendance:Key"];
            var hmasha256Token = CryptographyExtention.HmacSha256Encode(payload, secretKey);
            var payloadByteArray = Encoding.ASCII.GetBytes(payload);
            var payloadToken = payloadByteArray.ToHexString();
            var url = _configuration["Attendance:UrlCode"];
            return url + payloadToken + hmasha256Token;
        }

        public bool VerifyAttendanceToken(string attendanceToken)
        {
            if (attendanceToken.Length < Constant.ATTENDANCE_TOKEN_LENGTH)
            {
                return false;
            }

            var sha256Token = attendanceToken.Substring(attendanceToken.Length - Constant.SHA256_TOKEN_LENGTH, Constant.SHA256_TOKEN_LENGTH);
            //use payload and secretKey variable to generate new token to compare attendance token has been passed in
            var payload = attendanceToken.Substring(Constant.FIRST_INDEX_LENGTH, attendanceToken.Length - Constant.SHA256_TOKEN_LENGTH).FromHexString();
            var secretKey = _configuration["Attendance:Key"];
            var genetaSha256Token = CryptographyExtention.HmacSha256Encode(payload, secretKey);

            if (sha256Token != genetaSha256Token)
            {
                return false;
            }

            var currentTimestamp = new DateTimeOffset(_currentTime.GetCurrentTime()).ToUnixTimeSeconds();
            var expiredTimestamp = long.Parse(payload.Split(".")[Constant.SECOND_ELEMENT]);

            if (expiredTimestamp < currentTimestamp)
            {
                return false;
            }
         
            return true;
        }

        public KeyValuePair<Guid, int> GetDataByToken(string attendanceToken)
        {
            var isValidToken = VerifyAttendanceToken(attendanceToken);

            if (!isValidToken)
            {
                throw new AppException(Constant.INVALID_LINK);
            }

            var payload = attendanceToken.Substring(Constant.FIRST_INDEX_LENGTH, attendanceToken.Length - Constant.SHA256_TOKEN_LENGTH).FromHexString();
            var splitPayload = payload.Split(".");
            var fresherId = Guid.Parse(splitPayload[Constant.FIRST_ELEMENT]);
            var typeAttendance = int.Parse(splitPayload[Constant.THIRD_ELEMENT]);
            var fresher = new KeyValuePair<Guid, int>(fresherId, typeAttendance);
            return fresher;
        }
    }
}