using Domain.Entities;
using Global.Shared.Commons;
using Global.Shared.ViewModels.FeedbackViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IFeedbackRepository : IGenericRepository<Feedback>
    {
        public Task<Feedback?> GetFeedbackWithQuestionAndResultById(Guid id);

        public new Task<IList<Feedback>> GetAllAsync();

        public Task<Pagination<Feedback>> SearchAsync(SearchFeedbackViewModel searchFeedback);
    }
}
