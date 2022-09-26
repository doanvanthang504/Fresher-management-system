using Domain.Entities;
using Domain.Enums;
using Global.Shared.ViewModels.AuditManagementViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IAuditManagementRepository : IGenericRepository<AuditResult>
    {
        public Task<List<AuditResult>> GetAuditbyClassIdAsync(Guid classId);
        public Task<int> GetClassFreshersAsync(Guid classId);
        public Task<List<double>> GetScoreFromEvaluateAsync(List<Evaluate> listScoreEnum);
        public Task<List<AuditResult>> GetPlanAuditByClassIdAndNameModuelAsync(GetAuditByClassIdAndNumberAuditViewModel auditViewModel);
        public Task<List<Guid>> GetAuditorByClassModuleAsync(GetAuditByClassIdAndNumberAuditViewModel auditViewModel);
        public Task<int> GetNumberAuditorOfAuditAsync(GetAuditByClassIdAndNumberAuditViewModel getAuditByClassIdAnd);
    }
}
