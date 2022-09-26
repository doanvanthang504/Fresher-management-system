using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IModuleResultRepository : IGenericRepository<ModuleResult>
    {
        Task<ModuleResult?> GetModuleResultByFillterAsync(Expression<Func<ModuleResult, bool>> expression);
        Task<IList<ModuleResult>> GetModuleResultsByFillterAsync(Expression<Func<ModuleResult,bool>> expression);
    }
}
