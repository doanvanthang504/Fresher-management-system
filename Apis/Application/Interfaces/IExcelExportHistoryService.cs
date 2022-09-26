using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IExcelExportHistoryService
    {
        public Task<FileContentResult> ExportAsync<TParams>(TParams values);
    }
}
