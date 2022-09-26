using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using OfficeOpenXml;
using System.IO;
using System.Threading.Tasks;

namespace Global.Shared.ExportExcelExtensions
{
    public class SaveWorkBook
    {
        private readonly IHttpContextAccessor _httpContext;

        public SaveWorkBook(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public  async Task<FileContentResult> SaveFileAsync( ExcelPackage excelPackage, string fileName)
        {
            using var stream = new MemoryStream();
            await excelPackage.SaveAsAsync(stream);
            var content = stream.ToArray();
            var mediaTypeHeaderValue = new MediaTypeHeaderValue("application/octet-stream");
            _httpContext.HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "*");
            _httpContext.HttpContext.Response.Headers.Add("File-Name", fileName);
            var result = new FileContentResult(content, mediaTypeHeaderValue);
            return result;
        }
    }
}
