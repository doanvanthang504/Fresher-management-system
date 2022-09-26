using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IExcelExportChartService
    {
        public Task<FileContentResult> ExportAsync(Dictionary<string,float> values);
    }
}