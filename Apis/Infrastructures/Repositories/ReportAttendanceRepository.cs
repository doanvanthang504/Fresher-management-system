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
    public class ReportAttendanceRepository : GenericRepository<ReportAttendance>, IReportAttendanceRepository
    {

        public ReportAttendanceRepository(AppDbContext context, ICurrentTime timeService, IClaimsService claimsService) : base(context, timeService, claimsService)
        {
        }

        public async Task<List<ReportAttendance>> GetReportAttendanceListByClassAsync(List<Guid> freshersId, int month, int year)
        {
            return await _dbSet.Where(x => freshersId.Contains(x.FresherId) 
                                            && x.MonthAttendance == month 
                                            && x.YearAttendance == year).ToListAsync();
        }
    }
}
