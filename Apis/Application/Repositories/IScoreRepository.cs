using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IScoreRepository : IGenericRepository<Score>
    {
        Task<IList<Score>> GetScoresByFillterAsync(Expression<Func<Score, bool>> expression);
        Task<IList<TResult>> GetScoresByFillterAsync<TResult>(Expression<Func<Score, bool>> condition, 
                                                              Expression<Func<Score, TResult>> selector);
    }
}
