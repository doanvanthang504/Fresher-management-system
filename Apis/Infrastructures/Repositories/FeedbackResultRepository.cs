using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;
using Global.Shared.Commons;
using Global.Shared.Helpers;
using Global.Shared.ViewModels.FeedbackViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructures.Repositories
{
    public class FeedbackResultRepository : GenericRepository<FeedbackResult>, IFeedbackResultRepository
    {
        public FeedbackResultRepository(
            AppDbContext context,
            ICurrentTime timeService,
            IClaimsService claimsService)
            : base(context,
                  timeService,
                  claimsService)
        {
        }

      //  public Task AddRangeAsync(Task<List<FeedbackResult>> dataFeedbackResult)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<IList<FeedbackResult>> GetAllResultOfFeedbackAsync(Guid feedbackId)
        {
            return await _dbSet.Where(x => x.FeedbackId == feedbackId).ToListAsync();
        }

        public async Task<Pagination<FeedbackResult>> SearchAsync(SearchFeedbackResultViewModel searchFeedback)
        {
            var queryable = _dbSet.AsQueryable();
            Expression<Func<FeedbackResult, bool>> predicate = x => true;
            if (!string.IsNullOrEmpty(searchFeedback.AccountName))
            {
                Expression<Func<FeedbackResult, bool>> subExpression = x => x.AccountName
                                                                        .ToLower()
                                                                        .Contains(
                                                                            searchFeedback.AccountName.ToLower()
                                                                            );
                predicate = ExpressionHelper<FeedbackResult>.ExpressionCombineAndAlso(predicate, subExpression);
            }
            if (searchFeedback.QuestionId != null && searchFeedback.QuestionId != Guid.Empty)
            {
                Expression<Func<FeedbackResult, bool>> subExpression = x => x.QuestionId == searchFeedback.QuestionId;
                predicate = ExpressionHelper<FeedbackResult>.ExpressionCombineAndAlso(predicate, subExpression);
            }
            if (searchFeedback.CreationDate != null)
            {
                Expression<Func<FeedbackResult, bool>> subExpression =
                    x => x.CreationDate.Subtract(searchFeedback.CreationDate.Value).Days == 0;
                predicate = ExpressionHelper<FeedbackResult>.ExpressionCombineAndAlso(predicate, subExpression);
            }
            var result = await FindAsync(predicate, pageIndex: searchFeedback.PageIndex, pageSize: searchFeedback.PageSize);

            return result;
        }
    }
}
