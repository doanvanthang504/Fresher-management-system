using Application.Interfaces;
using Global.Shared.ViewModels.PlanInfomationViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class PlanInformationController : BaseController
    {
        private readonly IPlanInformationService _planInformationService;

        public PlanInformationController(
                        IPlanInformationService planInformationService)
        {
            _planInformationService = planInformationService;
        }

        [HttpGet("{classId}")]
        public async Task<IActionResult> GetByClassId([FromRoute] Guid classId)
        {
            var response = await _planInformationService.GetPlanDetailByClassIdAsync(classId);
            if (response == null) return NotFound();
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlanInformation(
                            [FromRoute] Guid id,
                            [FromBody] PlanInformationViewModel planInformationViewModel)
        {
            var response = await _planInformationService.UpdatePlanInfoAsync(
                                                        id, planInformationViewModel);
            if (response == false)
            {
                return NotFound();
            }
            return Ok();
        }
        [HttpGet("{classId}")]
        public async Task<List<WeightedNumberViewModel>> GetWeightedNumberOfModule([FromRoute]Guid classId)
        {
            var response = await _planInformationService.GetWeightedNumberOfModuleAsync(classId);
            return response;
        }
        [HttpPut("{classId}")]
        public async Task<WeightedNumberViewModel> UpdateWeightedNumberOfModule(
                                                    [FromRoute] Guid classId, 
                                                    WeightedNumberViewModel weightedNumberViewModel)
        {
            var response = await _planInformationService.UpdateWeightedNumberOfModuleAsync(
                                                                    classId, weightedNumberViewModel);
            return response;
        }
    }
}
