using Global.Shared.ModelExport.ModelExportConfiguration;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IExcelExportScroreService
    {
        public Task<FileContentResult> ExportAsync(List<ExportScoreViewModel> values);
    }
}