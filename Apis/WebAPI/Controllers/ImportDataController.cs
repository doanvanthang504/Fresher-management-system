using Application.Interfaces;
using Global.Shared.ViewModels.ImportViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/import-management")]
    public class ImportDataController : BaseController
    {
        private readonly IImportDataFromExcelFileService _importDataService;

        public ImportDataController(IImportDataFromExcelFileService importDataService)
        {
            _importDataService = importDataService;
        }

        [HttpPost("import-file-from-rec")]
        public async Task<PackageReponseFromRECFileImportViewModel?> GetDataFromImportedRECFile(IFormFile fileExcel)
        {
            var packageResponse = await _importDataService.GetDataFromRECExcelFileAsync(fileExcel);
            return packageResponse;
        }

        [HttpPost("import-file-delivery-plan")]
        public List<TrainingScheduleImportViewModel?> GetDataFromImportedTrainingScheduleExcelFile(
            IFormFile fileExcel,
            string scheduleSheetName)
        {
            var packageResponse = _importDataService.GetDataFromTrainingScheduleExcelFile(fileExcel,scheduleSheetName);
            return packageResponse;
        }

        [HttpPost("import-file-score")]
        public  List<ScoreImportViewModel>? GetScoreFromExcelFile(IFormFile fileExcel)
        {
            var packageResponse =  _importDataService.GetScoreFromExcelFile(fileExcel);
            return packageResponse;
        }
        
    }
}
