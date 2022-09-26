using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IQuestionManagementRepository : IGenericRepository<QuestionManagement>
    {
        public Task<List<QuestionManagement>> GetAllQuestionInPlanAuditAsync(byte numberAuditOfPlanAudit, string moduleName);
    }
}
