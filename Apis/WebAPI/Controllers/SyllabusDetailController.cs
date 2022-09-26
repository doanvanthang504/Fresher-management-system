using Application.Interfaces;
using Global.Shared.ViewModels.SyllabusDetailViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class SyllabusDetailController : BaseController
    {
        private readonly ISyllabusDetailService _syllabusDetailService;

        public SyllabusDetailController(ISyllabusDetailService syllabusDetailService)
        {
            _syllabusDetailService = syllabusDetailService;
        }

        [HttpGet("{classId}")]
        public async Task<IEnumerable<SyllabusDetailViewModel>> GetSyllabusDetailByClassId([FromRoute] Guid classId)
        {
            var response = await _syllabusDetailService.GetSyllabusDetailByClassIdAsync(classId);
            return response;
        }
        [HttpGet("{planInformationId}")]
        public async Task<IEnumerable<SyllabusDetailViewModel>> GetSyllabusDetailByPlanInformationId(
                                                                                    [FromRoute] Guid planInformationId)
        {
            var response = await _syllabusDetailService.GetSyllabusDetailByPlanInformationIdAsync(planInformationId);
            return response;
        }
        [HttpPut("{syllabusDetailId}")]
        public async Task<ActionResult<SyllabusDetailViewModel>> UpdateSyllabusDetail(
                              [FromRoute] Guid syllabusDetailId, SyllabusDetailAddViewModel syllabusDetailAddViewModel)
        {
            var response = await _syllabusDetailService.UpdateSyllabusDetailAsync(
                                                                         syllabusDetailId, syllabusDetailAddViewModel);
            return response;
        }
    }
}
