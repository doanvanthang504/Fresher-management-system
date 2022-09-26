using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace Global.Shared.ExportExcelExtensions
{
    public class ExcelHelper
    {
        private readonly ExcelWorksheet _worksheet;

        public ExcelHelper(ExcelWorksheet worksheet)
        {
            _worksheet = worksheet;
        }

        public void Merge(params string[] address)
        {
            for (int i = 0; i < address.Length; i++)
            {
                _worksheet.Cells[address[i]].Merge = true;
            }
        }

        public void AutoFitColumn()
        {
            _worksheet.Cells[_worksheet.Dimension.Address].AutoFitColumns();
            for (int col = 1; col <= _worksheet.Dimension.End.Column; col++)
            {
                _worksheet.Column(col).Width = _worksheet.Column(col).Width + 1;
            }
        }

        public void FillBackgroundColorCell( ExcelFillStyle excelFillStyle, Color color,params string[] address)
        {
            for (int i = 0; i < address.Length; i++)
            {
                _worksheet.Cells[address[i]].Style.Fill.PatternType = excelFillStyle;
                _worksheet.Cells[address[i]].Style.Fill.BackgroundColor.SetColor(color);
            }
        }

        public void FillBackgroundColorCell(ExcelFillStyle excelFillStyle, string hexColor, params string[] address)
        {
            var colorConverter = new ColorConverter();
            var color = (Color)colorConverter.ConvertFromString(hexColor);
            for (int i = 0; i < address.Length; i++)
            {
                _worksheet.Cells[address[i]].Style.Fill.PatternType = excelFillStyle;
                _worksheet.Cells[address[i]].Style.Fill.BackgroundColor.SetColor(color);
            }
        }

        public void SetBoldOnCells(params string[] address)
        {
            for (int i = 0; i < address.Length; i++)
            {
                _worksheet.Cells[address[i]].Style.Font.Bold = true;
            }
        }

        public void SetBoldOnRows(params int[] rows)
        {
            for (int i = 0; i < rows.Length; i++)
            {
                _worksheet.Row(rows[i]).Style.Font.Bold = true;
            }
        }

        public void SetBorderAroundStyle(ExcelBorderStyle style)
        {
            _worksheet.Cells[_worksheet.Dimension.Address].Style.Border.Bottom.Style = style;
            _worksheet.Cells[_worksheet.Dimension.Address].Style.Border.Top.Style = style;
            _worksheet.Cells[_worksheet.Dimension.Address].Style.Border.Left.Style = style;
            _worksheet.Cells[_worksheet.Dimension.Address].Style.Border.Right.Style = style;
        }

        public void SetBorderAroundStyle(ExcelBorderStyle style, params string[] address)
        {
            for (int i = 0; i < address.Length; i++)
            {
                _worksheet.Cells[address[i]].Style.Border.Bottom.Style = style;
                _worksheet.Cells[address[i]].Style.Border.Top.Style = style;
                _worksheet.Cells[address[i]].Style.Border.Left.Style = style;
                _worksheet.Cells[address[i]].Style.Border.Right.Style = style;
            }
        }

        public void SetFontSize(string address, int size)
        {
            _worksheet.Cells[address].Style.Font.Size = size;
        }

        public void SetFontSize(int size)
        {
            _worksheet.Cells[_worksheet.Dimension.Address].Style.Font.Size = size;
        }

        public void SetFontName(string addess, string fontName)
        {
            _worksheet.Cells[addess].Style.Font.Name = fontName;
        }

        public void SetFontName(string fontName)
        {
            _worksheet.Cells[_worksheet.Dimension.Address].Style.Font.Name = fontName;
        }

        public void FormatCell(string format, params string[] address)
        {
            for (int i = 0; i < address.Length; i++)
            {
                _worksheet.Cells[address[i]].Style.Numberformat.Format = format;
            }
        }

        public void FormatColumn(string format, params int[] columns)
        {
            for (int i = 0; i < columns.Length; i++)
            {
                _worksheet.Column(columns[i]).Style.Numberformat.Format = format;
            }
        }

        public void SetRowVerticalAlignmentCenter(params int[] rows)
        {
            for (int i = 0; i < rows.Length; i++)
            {
                _worksheet.Rows[rows[i]].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            }
        }

        public void Formula(string formula, params string[] address)
        {
            for (int i = 0; i < address.Length; i++)
            {
                _worksheet.Cells[address[i]].Formula = formula;
            }
        }
    }
}
