using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;
using Global.Shared.Commons;
using Global.Shared.Helpers;
using Global.Shared.ViewModels.FeedbackViewModels;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructures.Repositories
{
    public class FeedbackQuestionRepository : GenericRepository<FeedbackQuestion>, IFeedbackQuestionRepository
    {
        public FeedbackQuestionRepository(
            AppDbContext context,
            ICurrentTime timeService,
            IClaimsService claimsService)
            : base(context,
                  timeService,
                  claimsService)
        {
        }

        public async Task<Pagination<FeedbackQuestion>> SearchAsync(SearchFeedbackQuestionViewModel searchFeedback)
        {
            var queryable = _dbSet.AsQueryable();
            Expression<Func<FeedbackQuestion, bool>> predicate = x => true;
            if (!string.IsNullOrEmpty(searchFeedback.Title))
            {
                Expression<Func<FeedbackQuestion, bool>> subExpression =
                    x => x.Title.ToLower().Contains(searchFeedback.Title.ToLower());
                predicate = ExpressionHelper<FeedbackQuestion>.ExpressionCombineAndAlso(predicate, subExpression);
            }
            if (searchFeedback.CreationDate != null)
            {
                Expression<Func<FeedbackQuestion, bool>> subExpression =
                    x => x.CreationDate.Subtract(searchFeedback.CreationDate.Value).Days == 0;
                predicate = ExpressionHelper<FeedbackQuestion>.ExpressionCombineAndAlso(predicate, subExpression);
            }
            if (searchFeedback.FeedbackId != null && searchFeedback.FeedbackId != Guid.Empty)
            {
                Expression<Func<FeedbackQuestion, bool>> subExpression =
                    x => x.FeedbackId == searchFeedback.FeedbackId;
                predicate = ExpressionHelper<FeedbackQuestion>.ExpressionCombineAndAlso(predicate, subExpression);
            }
            var result = await FindAsync(predicate, pageIndex: searchFeedback.PageIndex, pageSize: searchFeedback.PageSize, includes: x => x.FeedbackAnswers);
            return result;
        }
    }
}
