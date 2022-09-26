using Application.Interfaces;
using Global.Shared.ViewModels.ModuleViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class ModuleController : BaseController
    {
        private readonly IModuleService _moduleService;

        public ModuleController(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllModule(int pageIndex = 0, int pageSize = 10)
        {
            return Ok(await _moduleService.GetAllModuleAsync(pageIndex, pageSize));
        }
        [HttpGet("{moduleId}")]
        public async Task<IActionResult> GetModuleById([FromRoute] Guid moduleId)
        {
            return Ok(await _moduleService.GetModuleByIdAsync(moduleId));
        }

        [HttpGet("{planId}")]
        public async Task<IActionResult> GetModuleByPlanId([FromRoute] Guid planId)
        {
            return Ok(await _moduleService.GetModuleByPlanIdAsync(planId));
        }

        [HttpPost]
        public async Task<IActionResult> AddModule(ModuleAddViewModel moduleAddViewModel)
        {
            return Ok(await _moduleService.AddModuleAsync(moduleAddViewModel));
        }
        [HttpPut("{moduleId}")]
        public async Task<IActionResult> UpdateModule([FromRoute] Guid moduleId, ModuleUpdateViewModel module)
        {
            return Ok(await _moduleService.UpdateModuleAsync(moduleId, module));
        }
    }
}
