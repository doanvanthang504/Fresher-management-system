using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructures.Repositories
{
    public class PlanInformationRepository : GenericRepository<PlanInformation>, IPlanInformationRepository
    {
        public PlanInformationRepository(AppDbContext dbContext,
                                ICurrentTime currentTime,
                                IClaimsService claimsService)
            : base(dbContext, currentTime, claimsService) { }

        public async Task<List<PlanInformation>> GetByClassIdAsync(Guid classId)
        {
           return await _dbSet.Where(x => x.ClassId == classId)
                              .ToListAsync();
        }

        public async Task<PlanInformation?> GetByModuleNameAndClassIdAsync(Guid classId, string moduleName)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.ClassId == classId 
                                                 && x.ModuleName == moduleName);
        }
    }

}
