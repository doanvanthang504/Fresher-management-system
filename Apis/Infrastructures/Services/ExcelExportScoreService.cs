using Application.Interfaces;
using Global.Shared.Commons;
using Global.Shared.ExportExcelExtensions;
using Global.Shared.ModelExport.ModelExportConfiguration;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructures.Services
{
    public class ExcelExportScroreService : IExcelExportScroreService
    {
        private readonly SaveWorkBook _saveWorkBook;
        private readonly ICurrentTime _currentTime;

        public ExcelExportScroreService(SaveWorkBook saveWorkBook, ICurrentTime currentTime)
        {
            _saveWorkBook = saveWorkBook;
            _currentTime = currentTime;
        }

        public async Task<FileContentResult> ExportAsync(List<ExportScoreViewModel> values)
        {
            using var excelPackage = new ExcelPackage();
            //Add Sheet
            var worksheet = excelPackage.Workbook.Worksheets.Add(Constant.SCORE);
            var excelHelper = new ExcelHelper(worksheet);
            //Fill Data
            worksheet.Cells["A1"].LoadFromCollection(values, true, TableStyles.Light11);

            //
            //  Configuration cells
            //
            var now = _currentTime.GetCurrentTime().ToShortDate();
            var fileName = Constant.SCORE + now + Constant.EXPORT_FILE_EXTENSION;
            return await _saveWorkBook.SaveFileAsync(excelPackage, fileName);
        }
    }
}
