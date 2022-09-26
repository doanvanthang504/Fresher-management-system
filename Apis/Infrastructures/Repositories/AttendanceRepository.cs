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
    public class AttendanceRepository : GenericRepository<Attendance>, IAttendanceRepository
    {
        public AttendanceRepository(
            AppDbContext context, 
            ICurrentTime timeService, 
            IClaimsService claimsService) : 
            base(context, timeService, claimsService)
        {
        }

        public async Task<List<Attendance>> GetAllAttendanceByFilterAsync(Expression<Func<Attendance, bool>> expression)
        {
            return await _dbSet.Include(x => x.Fresher).ThenInclude(x => x.ClassFresher).Where(expression).ToListAsync();
        }

        public async Task<Attendance?> GetAttendanceByFilterAsync(Expression<Func<Attendance, bool>> expression)
        {
            return await _dbSet.FirstOrDefaultAsync(expression);
        }
    }
}
