using Application.Interfaces;
using Global.Shared.Helpers;
using Global.Shared.ModelExport.ModelExportConfiguration;
using Global.Shared.ViewModels.ReportsViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class FresherReportController: BaseController
    {
        private readonly IFresherReportService _fresherReportService;

        public FresherReportController
            (IFresherReportService fresherReportService)
        {
            _fresherReportService = fresherReportService;
        }

        [HttpGet]
        public async Task<List<ExportCourseReportViewModel>> GetWeeklyFresherReportAsync
            ([FromHeader]GetWeeklyFresherReportFilterViewModel filter)
        {
            return await _fresherReportService.GetWeeklyFresherReportsByFilterAsync(filter);
        }

        [HttpPost]
        public async Task<string> CreateWeeklyFresherReportAsync
            ([FromForm]CreateWeeklyFresherReportViewModel model)
        {
            return await _fresherReportService.CreateWeeklyFresherReportAsync(model);
        }

        [HttpPut("{reportId}")]
        public async Task<string> UpdateWeeklyFresherReportAsync(
            Guid reportId,
            [FromForm] UpdateWeeklyFresherReportViewModel model)
        {
            return await _fresherReportService.UpdateWeeklyFresherReportAsync(reportId, model);
        }

        [HttpGet]
        public async Task<List<ExportCourseReportViewModel>> GetMonthlyReportsByFilterAsync
            ([FromQuery] GetFresherReportFilterViewModel filter)
        {
            return await _fresherReportService.GetMonthlyReportsByFilterAsync(filter);
        }

        [HttpPut("{reportId}")]
        public async Task<string> UpdateMonthlyReportAsync
            (Guid reportId, UpdateFresherReportViewModel updateInput)
        {
            return await _fresherReportService.UpdateMonthlyReportAsync(reportId, updateInput);
        }

        [HttpPost]
        public async Task<string> CreateMonthlyReportAsync
            (string courseCode)
        {
            return await _fresherReportService.CreateMonthlyReportAsync(courseCode);
        }

        [HttpGet]
        [AuthorizedFilter(Roles:"Admin")]
        public async Task<List<ExportCourseReportViewModel>?> GenerateFresherReportAsync
            ([FromQuery] bool isMonthly, int amount = 0)
        {
            var adminId = Guid.Parse(HttpContext.User
                                                .Claims
                                                .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            return await _fresherReportService.GenerateFresherReportAsync(adminId, isMonthly, amount);
        }
    }
}
