using Domain.Entities;
using Global.Shared.Commons;
using Global.Shared.ViewModels.FeedbackViewModels;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IFeedbackQuestionRepository : IGenericRepository<FeedbackQuestion>
    {
        public Task<Pagination<FeedbackQuestion>> SearchAsync(SearchFeedbackQuestionViewModel searchFeedback);
    }
}
