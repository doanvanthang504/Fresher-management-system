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
    public class ModuleRepository : GenericRepository<Module>,
                                    IModuleRepository
    {
        public ModuleRepository(AppDbContext dbContext,
                                ICurrentTime currentTime,
                                IClaimsService claimsService)
            : base(dbContext, currentTime, claimsService)
        {
        }

        public async Task<List<Module>> GetModuleByPlanId(Guid planId)
        {
            return await _dbSet.Where(x => x.PlanId == planId)
                               .Include(x => x.Topics)
                               .OrderBy(x => x.Order) //Thu tu tang dan
                               .ToListAsync();
        }
    }
}
