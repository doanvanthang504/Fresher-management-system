using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IFresherReportRepository: IGenericRepository<FresherReport>
    {
        Task<IList<FresherReport>> GetMonthlyReportsByFilterAsync(Expression<Func<FresherReport, bool>> expression);

        Task<IList<FresherReport>> GetWeeklyFresherReportsByFilterAsync(Expression<Func<FresherReport, bool>> expression);

        Task AddFresherReportAsync(FresherReport entity);

        void UpdateFresherReport(FresherReport entity);
    }
}