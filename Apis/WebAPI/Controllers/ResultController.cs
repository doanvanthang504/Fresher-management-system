using Application.Interfaces;
using Global.Shared.ViewModels.ModuleResultViewModels;
using Global.Shared.ViewModels.MonthResultViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class ResultController : BaseController
    {
        private readonly IModuleResultService _moduleResultService;
        private readonly IMonthResultService _monthResultService;
        public ResultController(IModuleResultService moduleResultService,
                                IMonthResultService monthResultService)
        {
            _moduleResultService = moduleResultService;
            _monthResultService = monthResultService;
        }

        [HttpGet]
        public async Task<IActionResult> GetModuleResult([FromQuery]GetModuleResultViewModel getModuleResultViewModel)
        {
            var moduleResultVM = await _moduleResultService
                .GetModuleResultByClassIdAndModuleNameAsync(getModuleResultViewModel.ClassId, getModuleResultViewModel.ModuleName);

            return Ok(moduleResultVM);
        }
        [HttpGet]
        public async Task<IActionResult> GetMonthResult([FromQuery]GetMonthResultViewModel getMonthResultViewModel)
        {
            var moduleResultVM = await _monthResultService.GetMonthResultByClassId(
                                                             getMonthResultViewModel.ClassId,
                                                             getMonthResultViewModel.Month,
                                                             getMonthResultViewModel.Year);

            return Ok(moduleResultVM);
        }
    }
}
