using Global.Shared.ViewModels.ImportViewModels;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IImportDataFromExcelFileService
    {
        public Task<PackageReponseFromRECFileImportViewModel?>
            GetDataFromRECExcelFileAsync(IFormFile? fileExcel);

        public Task<PackageReponseFromRECFileImportViewModel>
            CreateClassCodeFromPackageDataReponseAsync(PackageReponseFromRECFileImportViewModel package);

        public List<TrainingScheduleImportViewModel>
            GetDataFromTrainingScheduleExcelFile(IFormFile? fileExcel, string scheduleSheetName);

        public List<ScoreImportViewModel>? GetScoreFromExcelFile(IFormFile? fileExcel);
    }
}

