using Application.Interfaces;
using Global.Shared.Commons;
using Global.Shared.ExportExcelExtensions;
using Global.Shared.ModelExport.ModelExportConfiguration;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace Infrastructures.Services
{
    public class ExcelExportHistoryService : IExcelExportHistoryService
    {
        private readonly ICurrentTime _currentTime;
        private readonly Stream? _fileTemplate;
        private readonly SaveWorkBook _saveWorkBook;

        public ExcelExportHistoryService(ICurrentTime currentTime, SaveWorkBook saveWorkBook)
        {
            _currentTime = currentTime;
            _saveWorkBook = saveWorkBook;
            _fileTemplate = LoadExcelTemplate.GetStream(Constant.FILENAME_HISTORY_TEMPLATE);
        }

        public async Task<FileContentResult> ExportAsync<T>(T values)
        {
            var data = values as List<ExportCourseReportViewModel>;

            using var excelPackage = new ExcelPackage();
            Parallel.Invoke(
                () => AddWorkSheetTemplate(excelPackage),
                () => AddWorksheetOfCourse(excelPackage, data)
            );

            var now = _currentTime.GetCurrentTime().ToShortDate();
            var fileName = Constant.EXPORT_FILENAME_PREFIX_HISTORY + now + Constant.EXPORT_FILE_EXTENSION;
            return await _saveWorkBook.SaveFileAsync(excelPackage, fileName);
        }

        private void AddWorkSheetTemplate(ExcelPackage newExcelPackage)
        {
            if (_fileTemplate != null)
            {
                using var existExcelPackage = new ExcelPackage(_fileTemplate);

                var worksheetGuideline = existExcelPackage.Workbook.Worksheets[Constant.WORKSHEET_GUIDELINE];
                newExcelPackage.Workbook.Worksheets.Add(Constant.WORKSHEET_GUIDELINE, worksheetGuideline);

                var worksheetFinanceObligation = existExcelPackage.Workbook.Worksheets[Constant.WORKSHEET_FINANCE_OBLIGATION];
                newExcelPackage.Workbook.Worksheets.Add(Constant.WORKSHEET_FINANCE_OBLIGATION, worksheetFinanceObligation);

                var worksheetUniAndFalc = existExcelPackage.Workbook.Worksheets[Constant.WORKSHEET_UNI_AND_FALCULTY_LIST];
                newExcelPackage.Workbook.Worksheets.Add(Constant.WORKSHEET_UNI_AND_FALCULTY_LIST, worksheetUniAndFalc);

                var worksheetRecordOfChange = existExcelPackage.Workbook.Worksheets[Constant.WORKSHEET_RECORD_OF_CHANGES];
                newExcelPackage.Workbook.Worksheets.Add(Constant.WORKSHEET_RECORD_OF_CHANGES, worksheetRecordOfChange);
            }
        }
        private static void AddWorksheetOfCourse(ExcelPackage excelPackage, List<ExportCourseReportViewModel>? values)
        {
            //Add Sheet
            var worksheet = excelPackage.Workbook.Worksheets.Add(Constant.COURSES);
            var excelHelper = new ExcelHelper(worksheet);

            worksheet.Cells["A1"].Value = Constant.COURSES;

            //Add Header
            worksheet.Cells["A2"].Value = Constant.EMPLOYEE_INFO_HEADER;
            worksheet.Cells["N2"].Value = Constant.COURSE_INFO_HEADER;
            worksheet.Cells["U2"].Value = Constant.TRAINEE_INFO_HEADER;
            worksheet.Cells["AI2"].Value = Constant.VALIDATION_AND_SUPPORT_INFO_HEADER;

            //Fill Data
            worksheet.Cells["A3"].LoadFromCollection(values, true, TableStyles.Light11);

            //
            //  Configuration cells
            //

            worksheet.Row(2).Height = 24;
            worksheet.Row(3).Height = 30;

            excelHelper.SetFontName(Constant.FONT_NAME_ARIAL);

            excelHelper.FormatColumn(Constant.DATE_TIME_FORMAT_ddMMMyy, 21, 22, 34, 42, 43);

            excelHelper.SetRowVerticalAlignmentCenter(2, 3);

            excelHelper.SetBorderAroundStyle(ExcelBorderStyle.Thin);

            excelHelper.SetFontSize("A1", 15);
            excelHelper.SetBoldOnRows(1, 2, 3);
            excelHelper.FillBackgroundColorCell(ExcelFillStyle.Solid, Color.LightGray, "A2:AT2");
            excelHelper.AutoFitColumn();

            excelHelper.Merge("A1:AT1", "A2:M2", "N2:T2", "U2:AH2", "AI2:AT2");

        }
    }
}
