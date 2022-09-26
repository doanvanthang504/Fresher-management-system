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
    public class ClassFresherRepository : GenericRepository<ClassFresher>, IClassFresherRepository
    {
        public ClassFresherRepository(AppDbContext DbContext,
            ICurrentTime currentTime,
            IClaimsService claimsService) :
            base(DbContext,
                currentTime,
                claimsService)
        {
        }

        public async Task<ClassFresher> GetClassWithFresherByClassIdAsync(Guid classId)
        {
            return await _dbSet.Where(x => x.Id == classId).Include(x => x.Freshers).FirstOrDefaultAsync();
        }

        public async Task<ClassFresher?> GetClassIncludeFreshersByIdAsync(Guid classId)
        {
            return await _dbSet.Include(x => x.Freshers)
                .ThenInclude(x => x.Attendances)
                .FirstOrDefaultAsync(x => x.Id == classId);
        }

        public async Task<List<string>> GetAllClassCodeAsync()
        {
            return await _dbSet.Select(x => x.ClassCode).ToListAsync();
        }

        public async Task<bool> CheckExistedClassAsync(string rrCode)
        {
            return await _dbSet.AnyAsync(x => x.RRCode.Equals(rrCode));
        }

        public Task<ClassFresher?> GetClassIncludeFreshersAttendancesByIdAsync(Guid classId, int month, int year)
        {
            return _dbSet.Include(e => e.Freshers)
                         .ThenInclude(e => e.Attendances
                                           .Where(x => x.AttendDate1.Month == month
                                                    && x.AttendDate1.Year == year
                                                    && x.AttendDate2.Month == month
                                                    && x.AttendDate2.Year == year))
                         .FirstOrDefaultAsync(x=>x.Id == classId);
        }

        public async Task<ClassFresher?> GetClassFresherByClassCodeAsync(string classCode)
            => await _dbSet.Where(c => c.ClassCode == classCode)
                           .Include(c => c.Freshers)
                           .FirstOrDefaultAsync();

     

        public async Task<List<ClassFresher>?> GetClassFresherByAdminNameAsync(string adminName)
            => await _dbSet.Where(c => c.NameAdmin1 == adminName
                                    || c.NameAdmin2 == adminName
                                    || c.NameAdmin3 == adminName)
                           .Include(c => c.Freshers)
                           .ToListAsync();
    }
}
