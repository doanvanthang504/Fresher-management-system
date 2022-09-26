using Domain.Entities;
using Domain.Enums;
using Global.Shared.ViewModels;
using Global.Shared.ViewModels.AuditManagementViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAuditManagementService
    {
        public Task<AuditManagementResponse> UpdateAuditForStudentAsync(Guid id,UpdateAuditViewModel updateAuditViewModel);
        public Task<AuditManagementResponse> GetAuditByClassIdAsync(Guid classId);
        public Task<AuditManagementResponse> DeleteAuditResultAsync(Guid Id);
        public Task<AuditManagementResponse> AddAuditorForPlanAuditAsync(GetAuditAndAuditorViewModel getAuditAndAuditor);
        public Task<AuditManagementResponse> GetAllAuditPlansAsync();
        public Task<AuditManagementResponse> GetDetailPlanAuditAsync(GetAuditByClassIdAndNumberAuditViewModel auditViewModel);
        public Task<AuditManagementResponse> GetAllAuditorAsync();
        public Task<List<AuditorViewModel>> GetAuditorOfClassModuleAsync(GetAuditByClassIdAndNumberAuditViewModel auditViewModel);
        public Task<AuditManagementResponse> GetAllFresherIdInClassAsync(Guid classId);
        public Task<AuditManagementResponse> CreatePlanAuditForMemberInClassAsync(CreateAuditViewModel createPlanAudit);
        public Task<AuditManagementResponse> CountAuditorOfClassAsync(GetAuditByClassIdAndNumberAuditViewModel getAuditByClassIdAndNumberAudit);
        public Task<AuditManagementResponse> UpdateInformationPlanAuditAsync(UpdateInformationPlanAudit updateInformationPlanAudit);
        public Task<AuditResultResponse> GetAuditResultOfFresherInModuleAsync(AuditResultOfFresherInModuleViewModel getAuditResultOfFresher);
    }
}
