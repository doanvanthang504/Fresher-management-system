using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repositories
{
    public class FresherReportRepository : GenericRepository<FresherReport>, IFresherReportRepository
    {
        public FresherReportRepository(
            AppDbContext context, 
            ICurrentTime timeService, 
            IClaimsService claimsService) : base(context, timeService, claimsService)
        {
        }

        public async Task<IList<FresherReport>> GetMonthlyReportsByFilterAsync
            (Expression<Func<FresherReport, bool>> expression)
            => await _dbSet.Where(expression).ToListAsync();

        public async Task<IList<FresherReport>> GetWeeklyFresherReportsByFilterAsync
            (Expression<Func<FresherReport, bool>> expression)
            => await _dbSet.Where(expression).ToListAsync();

        public async Task AddFresherReportAsync(FresherReport entity)
        {
            Update(entity);
            entity.UpdatedDate = entity.ModificationDate;
            await _dbSet.AddAsync(entity);
        }

        public void UpdateFresherReport(FresherReport entity)
        {
            Update(entity);
            entity.UpdatedDate = entity.ModificationDate;
            _dbSet.Update(entity);
        }
    }
}
