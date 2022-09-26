using Application.Interfaces;
using Global.Shared.Commons;
using Global.Shared.ExportExcelExtensions;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Reflection;
using System.IO;
using System.Threading.Tasks;

namespace Infrastructures.Services
{
    public class ExcelExportDeliveryService : IExcelExportDeliveryService
    {
        private readonly ICurrentTime _currentTime;
        private readonly Stream? _fileTemplate;
        private readonly SaveWorkBook _saveWorkBook;

        public ExcelExportDeliveryService(ICurrentTime currentTime, SaveWorkBook saveWorkBook)
        {
            _currentTime = currentTime;
            _saveWorkBook = saveWorkBook;
            _fileTemplate = LoadExcelTemplate.GetStream(Constant.FILENAME_DELIVERY_TEMPLATE);
        }

        public async Task<FileContentResult> ExportAsync()
        {
            using ExcelPackage excelPackage = new();
            AddAllWorkSheet(excelPackage);
            var now = _currentTime.GetCurrentTime().ToShortDate();
            var fileName = Constant.EXPORT_FILENAME_PREFIX_DELIVERY + now + Constant.EXPORT_FILE_EXTENSION;
            return await _saveWorkBook.SaveFileAsync(excelPackage, fileName);
        }

        private void AddAllWorkSheet(ExcelPackage newExcelPackage)
        {
            if (_fileTemplate != null)
            {
                using var existExcelPackage = new ExcelPackage(_fileTemplate);

                var guidelineWorksheet = existExcelPackage.Workbook.Worksheets[Constant.WORKSHEET_GUIDELINE];
                var courseWorksheet = existExcelPackage.Workbook.Worksheets[Constant.WORKSHEET_COURSES_SEMINARS_WORKSHOPS];
                var examAndCertificateSupportWorksheet = existExcelPackage.Workbook.Worksheets[Constant.WORKSHEET_EXAMS_AND_CERTIFICATE_SUPPORT];
                var recordOfChangeWorksheet = existExcelPackage.Workbook.Worksheets[Constant.WORKSHEET_RECORD_OF_CHANGES];

                newExcelPackage.Workbook.Worksheets.Add(Constant.WORKSHEET_GUIDELINE, guidelineWorksheet);
                newExcelPackage.Workbook.Worksheets.Add(Constant.WORKSHEET_COURSES_SEMINARS_WORKSHOPS, courseWorksheet);
                newExcelPackage.Workbook.Worksheets.Add(Constant.WORKSHEET_EXAMS_AND_CERTIFICATE_SUPPORT, examAndCertificateSupportWorksheet);
                newExcelPackage.Workbook.Worksheets.Add(Constant.WORKSHEET_RECORD_OF_CHANGES, recordOfChangeWorksheet);
            }
        }
    }
}
