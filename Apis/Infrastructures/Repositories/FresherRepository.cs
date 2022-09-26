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
    public class FresherRepository : GenericRepository<Fresher>, IFresherRepository
    {
        public FresherRepository(AppDbContext DbContext,
            ICurrentTime currentTime,
            IClaimsService claimsService) :
            base(DbContext,
                currentTime,
                claimsService)
        {
        }

        public async Task<List<Fresher>> GetFresherByClassCodeAsync(string classCode)
        {
            var listFresher = await _dbSet.Where(x => x.ClassCode.ToLower()
                                           .Equals(classCode.ToLower())).ToListAsync();
            return listFresher;
        }

        public async Task<bool> CheckExistedFresherByAccountNameAsync(string accountName)
        {
            return await _dbSet.AnyAsync(x => x.AccountName.ToLower().Equals(accountName.ToLower()));
        }
        public async Task<List<Fresher>> GetFresherByClassIdAsync(Guid classId, string moduleName)
        {
            var listFresher = await _dbSet.Where(x => x.ClassFresherId == classId)
                                      .Include(x => x.ModuleResults.Where(x => x.ModuleName == moduleName))
                                      .ToListAsync();
            return listFresher;
        }

        public async Task<List<Fresher>> GetFresherByClassIdAsync(Guid classId)
        {
            var listFresher = await _dbSet.Where(x => x.ClassFresherId == classId)
                                      .ToListAsync();
            return listFresher;
        }
    }
}
