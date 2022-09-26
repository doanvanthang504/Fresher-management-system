using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repositories
{
    public class QuestionManagementRepository : GenericRepository<QuestionManagement>, IQuestionManagementRepository
    {
        public QuestionManagementRepository(AppDbContext context, 
                                            ICurrentTime timeService, 
                                            IClaimsService claimsService
                                           ) : base(context, timeService, claimsService)
        {
        }
        public async Task<List<QuestionManagement>> GetAllQuestionInPlanAuditAsync(byte numberAudit, string moduleName)
        {
            var result = await _dbSet.Where(x => x.NumberAudit == numberAudit && x.ModuleName == moduleName).ToListAsync();
            return result;
        }
    }
}
