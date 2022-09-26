using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IExcelExportDeliveryService
    {
        public Task<FileContentResult> ExportAsync();
    }
}