using Application.Interfaces;
using Global.Shared.ViewModels.PlanInfomationViewModels;
using Global.Shared.ViewModels.PlanViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class PlanController : BaseController
    {
        private readonly IPlanService _planService;

        public PlanController(IPlanService planService)
        {
            _planService = planService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPlan(int pageIndex = 0, int pageSize = 10)
        {
            return Ok(await _planService.GetAllPlanAsync(pageIndex, pageSize));
        }
        [HttpGet("{planId}")]
        public async Task<IActionResult> GetPlanById([FromRoute] Guid planId)
        {
            return Ok(await _planService.GetPlanByIdAsync(planId));
        }

        [HttpPost]
        public async Task<IActionResult> AddPlan(PlanAddViewModel planAddViewModel)
        {
            return Ok(await _planService.AddPlanAsync(planAddViewModel));
        }
        [HttpPut("{planId}")]
        public async Task<IActionResult> UpdatePlan([FromRoute] Guid planId, PlanUpdateViewModel plan)
        {
            return Ok(await _planService.UpdatePlanAsync(planId, plan));
        }

        [HttpPost]
        public async Task<IActionResult> ChoosePlanForClassAsync(ChoosePlanForClassViewModel planInfomation)
        {
            return Ok(await _planService.ChoosePlanForClassAsync(planInfomation));
        }
    }
}
