using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructures.Repositories
{
    internal class ModuleResultRepository : GenericRepository<ModuleResult>, IModuleResultRepository
    {
        public ModuleResultRepository(
                                    AppDbContext context,
                                    ICurrentTime timeService,
                                    IClaimsService claimsService)
                        : base(context, timeService, claimsService)
        {
        }
        public async Task<ModuleResult?> GetModuleResultByFillterAsync(Expression<Func<ModuleResult, bool>> expression)
        {
            return await _dbSet.FirstOrDefaultAsync(expression);
        }

        public async Task<IList<ModuleResult>> GetModuleResultsByFillterAsync(Expression<Func<ModuleResult, bool>> expression)
        {
            return await _dbSet.AsNoTracking().Where(expression).ToListAsync();
        }
    }
}
