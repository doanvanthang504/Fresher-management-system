using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Global.Shared.ViewModels;
using Global.Shared.ViewModels.AuditManagementViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class AuditResultController : BaseController
    {
        private readonly IAuditManagementService _auditManagementService;
        public AuditResultController(IAuditManagementService auditManagementService)
        {
            _auditManagementService = auditManagementService;
        }        
        // Update detail information of audit
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuditForStudent([FromRoute] Guid id, UpdateAuditViewModel addAuditViewModel)
        {
            var result = await _auditManagementService.UpdateAuditForStudentAsync(id, addAuditViewModel);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        // Get plan audit of class
        [HttpGet("{classId}")]
        public async Task<IActionResult> GetAuditByAuditor(Guid classId)
        {
            var result = await _auditManagementService.GetAuditByClassIdAsync(classId);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> DeleteAuditResult(Guid id)
        {
            var result = await _auditManagementService.DeleteAuditResultAsync(id);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        // add auditor for plan audit
        [HttpPut]
        public async Task<IActionResult> AddAduditorForPlanAudit(GetAuditAndAuditorViewModel addAuditorForPlanAudit)
        {
            var result = await _auditManagementService.AddAuditorForPlanAuditAsync(addAuditorForPlanAudit);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        // Get Plan Audit
        [HttpGet]
        public async Task<IActionResult> GetPlanAudit()
        {
            var result = await _auditManagementService.GetAllAuditPlansAsync();
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        // get detail information of plan audit
        [HttpPost]
        public async Task<IActionResult> GetDetailPlanAuditAsync(GetAuditByClassIdAndNumberAuditViewModel auditViewModel)
        {
            var result = await _auditManagementService.GetDetailPlanAuditAsync(auditViewModel);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        // get all account have role Auditor
        [HttpGet]
        public async Task<IActionResult> GetAllAuditAsync()
        {
            var result = await _auditManagementService.GetAllAuditorAsync();
            if(result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        // Get All auditor of plan audit
        [HttpPost]
        public async Task<IActionResult> GetAuditorOfClassModule(GetAuditByClassIdAndNumberAuditViewModel auditViewModel)
        {
            var result = await _auditManagementService.GetAuditorOfClassModuleAsync(auditViewModel);
            return Ok(result);
        }
        // Get Fresher in class
        [HttpGet]
        public async Task<IActionResult> GetFresherInClass(Guid classId)
        {
            var result = await _auditManagementService.GetAllFresherIdInClassAsync(classId);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        // Create plan audit for member class
        [HttpPost]
        public async Task<IActionResult> CreatePlanAuditForMemberInClassAsync([FromBody] CreateAuditViewModel createPlanAudit)
        {
            var result = await _auditManagementService.CreatePlanAuditForMemberInClassAsync(createPlanAudit);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        // Get Count Auditor of class Audit
        [HttpPost]
        public async Task<IActionResult> GetCountAuditorOfClassAudit([FromBody] GetAuditByClassIdAndNumberAuditViewModel getCountAuditorOfClassAudit)
        {
            var result = await _auditManagementService.CountAuditorOfClassAsync(getCountAuditorOfClassAudit);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        // update information of plan audit
        [HttpPut]
        public async Task<IActionResult> UpdateInformationOfPlanAudit(UpdateInformationPlanAudit updateInformationOfPlanAudit)
        {
            var result = await _auditManagementService.UpdateInformationPlanAuditAsync(updateInformationOfPlanAudit);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpPost]
        public async Task<IActionResult> GetAuditResultOfFresherInModule(AuditResultOfFresherInModuleViewModel auditResultOfFresherInModuleViewModel)
        {
            var result = await _auditManagementService.GetAuditResultOfFresherInModuleAsync(auditResultOfFresherInModuleViewModel);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
