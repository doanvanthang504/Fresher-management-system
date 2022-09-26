using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructures.Repositories
{
    public class ScoreRepository : GenericRepository<Score>, IScoreRepository
    {
        public ScoreRepository(AppDbContext dbContext,
                                      ICurrentTime timeService,
                                      IClaimsService claimsService)
                                    : base(dbContext, timeService, claimsService)
        {
        }
        public async Task<IList<Score>> GetScoresByFillterAsync(Expression<Func<Score, bool>> expression)
        {
            return await _dbSet.Where(expression).ToListAsync();
        }
        public async Task<IList<TResult>> GetScoresByFillterAsync<TResult>(Expression<Func<Score, bool>> condition, Expression<Func<Score, TResult>> selector)
        {
            return await _dbSet.Where(condition).Select(selector).ToListAsync();
        }
    }
}
