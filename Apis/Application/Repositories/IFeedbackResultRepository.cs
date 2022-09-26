using Domain.Entities;
using Global.Shared.Commons;
using Global.Shared.ViewModels.FeedbackViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IFeedbackResultRepository : IGenericRepository<FeedbackResult>
    {
        public Task<IList<FeedbackResult>> GetAllResultOfFeedbackAsync(Guid feedbackId);

        public new Task<Pagination<FeedbackResult>> SearchAsync(SearchFeedbackResultViewModel searchFeedback);
        //Task AddRangeAsync(Task<List<FeedbackResult>> dataFeedbackResult);
    }
}
