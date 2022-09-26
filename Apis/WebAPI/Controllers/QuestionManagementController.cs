using Application.Interfaces;
using Domain.Entities;
using Global.Shared.ViewModels.QuestionManagementViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class QuestionManagementController : BaseController
    {
        private readonly IQuestionManagementService _questionManagementService;
        public QuestionManagementController( IQuestionManagementService questionManagementService)
        {
            _questionManagementService = questionManagementService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateListQuestionForPlanAudit(PostQuestionViewModel postQuestionViewModel)
        {
            var result = await _questionManagementService.AddRangeAsync(postQuestionViewModel);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpPost]
        public async Task<IActionResult> GetAllQuestionInPlanAudit(GetQuestionToServer getQuestionToSerrver)
        {
            var result = await _questionManagementService.GetAllQuestionInPlanAuditAsync(getQuestionToSerrver);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
