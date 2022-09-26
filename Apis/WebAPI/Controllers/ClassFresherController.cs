using Application.Interfaces;
using Global.Shared.Commons;
using Global.Shared.ViewModels;
using Global.Shared.ViewModels.ClassFresherViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class ClassFresherController : BaseController
    {
        private readonly IClassFresherService _classFresherService;

        public ClassFresherController(IClassFresherService classFresherService)
        {
            _classFresherService = classFresherService;
        }

        [HttpGet]
        public async Task<Pagination<ClassFresherViewModel>> GetAllClassFresherPagingsionAsync([FromQuery] PaginationRequest request)
        {
            return await _classFresherService.GetAllClassFreshersPagingsionAsync(request);
        }

        [HttpGet("{classId}")]
        public async Task<ClassFresherViewModel> GetClassFresherByIdAsync(Guid classId)
        {
            return await _classFresherService.GetClassFresherByClassIdAsync(classId);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateClassFresherInfomationAsync([FromBody] UpdateClassFresherInfoViewModel updateClassFresherViewModel)
        {
            _ = await _classFresherService.UpdateClassFresherAfterImportedAsync(updateClassFresherViewModel);
            return Ok(Constant.UPDATE_CLASS_INFO_SUCCESS);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateClassFresherAsync([FromBody] UpdateClassFresherViewModel updateClassFresherView)
        {
            _ = await _classFresherService.UpdateClassFresher(updateClassFresherView);
            return Ok(Constant.UPDATE_CLASS_INFO_SUCCESS);
        }

        [HttpGet("{classCode}")]
        public async Task<List<FresherViewModel>> GetFresherByClassCodeAsync(string classCode)
        {
            return await _classFresherService.GetFreshersByClassCodeAsync(classCode);
        }

        [HttpPost]
        public async Task<List<ClassFresherViewModel>> CreateClassFresherFromImportedFileAsync(IFormFile fileExcel)
        {
            return await _classFresherService.CreateClassFresherFromImportedExcelFile(fileExcel);
        }

        [HttpGet("{classId}")]
        public async Task<ClassFresherViewModel> GetClassWithFresherByClassIdAsync(Guid classId)
        {
            return await _classFresherService.GetClassWithFresherByClassIdAsync(classId);
        }

        [HttpGet]
        public async Task<List<string>> GetAllClassCodeAsync()
        {
            return await _classFresherService.GetAllClassCodeAsync();
        }

        [HttpDelete("{classFresherId}")]
        public async Task<bool> DeleteClassFresherAsync(Guid classFresherId)
        {
            return await _classFresherService.DeleteClassFresherAsync(classFresherId);
        }
    }
}
