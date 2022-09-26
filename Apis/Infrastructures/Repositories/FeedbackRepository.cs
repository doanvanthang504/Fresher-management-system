using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;
using Global.Shared.Commons;
using Global.Shared.Helpers;
using Global.Shared.ViewModels.FeedbackViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructures.Repositories
{
    public class FeedbackRepository : GenericRepository<Feedback>, IFeedbackRepository
    {
        public FeedbackRepository(
            AppDbContext context,
            ICurrentTime timeService,
            IClaimsService claimsService)
            : base(context,
                  timeService,
                  claimsService)
        {
        }

        public async Task<Feedback?> GetFeedbackWithQuestionAndResultById(Guid id)
        {
            return await _dbSet.Where(x => x.Id == id)
                                .Include(x => x.FeedbackResults)
                                .Include(x => x.FeedbackQuestions)
                                .AsNoTracking()
                                .FirstOrDefaultAsync();
        }

        public async Task<Pagination<Feedback>> SearchAsync(SearchFeedbackViewModel searchFeedback)
        {
            var queryable = _dbSet.AsQueryable();
            Expression<Func<Feedback, bool>> predicate = x => true;
            if (!string.IsNullOrEmpty(searchFeedback.Title))
            {
                Expression<Func<Feedback, bool>> subExpression = x => x.Title
                                                                        .ToLower()
                                                                        .Contains(
                                                                            searchFeedback.Title.ToLower()
                                                                            );
                predicate = ExpressionHelper<Feedback>.ExpressionCombineAndAlso(predicate, subExpression);
            }
            if (searchFeedback.CreationDate != null)
            {
                Expression<Func<Feedback, bool>> subExpression =
                    x => x.CreationDate.Subtract(searchFeedback.CreationDate.Value).Days == 0;
                predicate = ExpressionHelper<Feedback>.ExpressionCombineAndAlso(predicate, subExpression);
            }
            var result = await FindAsync(predicate, pageIndex: searchFeedback.PageIndex, pageSize: searchFeedback.PageSize);

            return result;
        }
    }
}
