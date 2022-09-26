using Application.Interfaces;
using Global.Shared.ViewModels;
using Global.Shared.ViewModels.AttendancesViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class AttendanceController : BaseController
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpPost]
        public async Task<bool> SendAttendanceEmailToFresher(GenerateAttendanceClassTokenViewModel attendance)
        {
            return await _attendanceService.SendAttendanceEmailToFresherAsync(attendance);
        }

        [HttpGet("{token}")]
        public async Task<bool> TakeAttendance([FromRoute]string token)
        {
            return await _attendanceService.TakeAttendanceAsync(token);
        }

        [HttpPut]
        public async Task<AttendanceViewModel?> UpdateAttendance([FromBody]UpdateAttendanceViewModel attendance)
        {
            return await _attendanceService.UpdateAttendanceAsync(attendance);
        }

        [HttpGet]
        public async Task<List<AttendanceViewModel>> ListAttendanceOfFresher([FromQuery]FilterAttendanceViewModel attendance)
        {
            return await _attendanceService.GetAllAttendanceByFresherIdAsync(attendance);   
        }

        [HttpGet]
        public async Task<List<FresherAttendancesViewModel>> ListAttendanceOfFresherByClass([FromQuery]FilterAttendanceViewModel attendance)
        {
            return await _attendanceService.GetAllAttendanceByClassIdAsync(attendance);
        }

        [HttpPost]
        public async Task<string> CreateLinkAttendanceClass([FromBody]GenerateAttendanceClassTokenViewModel attendance)
        {
            return await _attendanceService.GenenerateLinkAttendanceAsync(attendance);
        }

        [HttpGet("{token}")]
        public async Task<bool> TakeAttendanceClass([FromRoute] string token, string tokenUser)
        {
            return await _attendanceService.TakeAttendanceClassAsync(token, tokenUser);
        }

        [HttpGet("{token}")]
        public bool ValidateAttendanceToken(string token)
        {
            return _attendanceService.IsValidAttendanceToken(token);
        }

    }
}

