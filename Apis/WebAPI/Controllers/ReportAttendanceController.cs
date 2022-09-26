using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Global.Shared.Commons;
using Global.Shared.Helpers;
using Global.Shared.ViewModels;
using Global.Shared.ViewModels.AttendancesViewModels;
using Global.Shared.ViewModels.AuthViewModels;
using Global.Shared.ViewModels.ChemicalsViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class ReportAttendanceController : BaseController
    {
        private readonly IReportAttendanceService _reportAttendanceService;
        public ReportAttendanceController(IReportAttendanceService reportAttendanceService)
        {
            _reportAttendanceService = reportAttendanceService;
        }

        [HttpPost]
        public async Task<List<ReportAttendanceViewModel>> CreateReportAttendanceByClass(CreateReportAttendanceViewModel request)
        {
            return await _reportAttendanceService.CreateReportAttendanceByClass(request);
        }

        [HttpGet]
        public async Task<List<ReportAttendanceViewModel>> GetAllReportAttendance()
        {
            return await _reportAttendanceService.GetAllReportAttendanceAsync();
        }
    }
}

