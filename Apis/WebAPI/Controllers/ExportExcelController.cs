using Application.Interfaces;
using Global.Shared.Helpers;
using Global.Shared.ModelExport.ModelExportConfiguration;
using Infrastructures.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace WebAPI.Controllers
{
    [ControllerName("export")]
    public class ExportExcelController : BaseController
    {
        private readonly IExcelExportHistoryService _excelExportHistoryService;
        private readonly IExcelExportDeliveryService _excelExportDeliveryService;
        private readonly IExcelExportChartService _excelExportChartService;
        private readonly IExcelExportScroreService _excelExportScoreService;

        public ExportExcelController(
                            IExcelExportHistoryService excelExportHistoryService,
                            IExcelExportDeliveryService excelExportDeliveryService,
                            IExcelExportChartService excelExportChartService,
                            IExcelExportScroreService excelExportScoreService)
        {
            _excelExportHistoryService = excelExportHistoryService;
            _excelExportDeliveryService = excelExportDeliveryService;
            _excelExportChartService = excelExportChartService;
            _excelExportScoreService = excelExportScoreService;
        }

        [HttpPost]
        [ActionName("employee-training-history")]
        public async Task<IActionResult> ExportEmployeeTrainingHistoryAsync(
            [FromBody] List<ExportCourseReportViewModel> values)
        {
            return await _excelExportHistoryService.ExportAsync(values);
        }

        [HttpPost]
        [ActionName("employee-training-delivery")]
        public async Task<IActionResult> ExportEmployeeTrainingDeliveryAsync()
        {
            return await _excelExportDeliveryService.ExportAsync();
        }

        [HttpPost]
        [ActionName("score")]
        public async Task<IActionResult> ExportScoreAsync([FromBody] List<ExportScoreViewModel> data)
        {
            return await _excelExportScoreService.ExportAsync(data);
        }

        [HttpPost]
        [ActionName("chart")]
        public async Task<IActionResult> ExportChartAsync([FromBody] Dictionary<string, float> data)
        {
            return await _excelExportChartService.ExportAsync(data);
        }
    }
}
