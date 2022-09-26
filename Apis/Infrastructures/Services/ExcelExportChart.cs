using Application.Interfaces;
using Global.Shared.Commons;
using Global.Shared.ExportExcelExtensions;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructures.Services
{
    public class ExcelExportChartService : IExcelExportChartService
    {
        private readonly SaveWorkBook _saveWorkBook;

        public ExcelExportChartService(SaveWorkBook saveWorkBook)
        {
            _saveWorkBook = saveWorkBook;
        }

        public async Task<FileContentResult> ExportAsync(Dictionary<string, float> values)
        {
            using ExcelPackage excelPackage = new();
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add(Constant.CHART);

            var i = 1;
            foreach (var item in values)
            {
                worksheet.Cells[1, i].Value = item.Key;
                worksheet.Cells[2, i].Value = item.Value;
                i++;
            }

            ExcelPieChart? pieChart = worksheet.Drawings.AddChart(Constant.PIE_CHART, eChartType.Pie3D) as ExcelPieChart;

            pieChart.Title.Text = Constant.PIE_CHART;

            pieChart.Series.Add(ExcelCellBase.GetAddress(2, 1, 2, 10), ExcelRange.GetAddress(1, 1, 1, 10));

            pieChart.Legend.Position = eLegendPosition.Right;

            pieChart.DataLabel.ShowPercent = true;
            pieChart.DataLabel.ShowValue = true;

            pieChart.SetSize(500, 400);
            pieChart.SetPosition(4, 0, 2, 0);
            var fileName = string.Empty;
            return await _saveWorkBook.SaveFileAsync(excelPackage, fileName);
        }
    }
}
