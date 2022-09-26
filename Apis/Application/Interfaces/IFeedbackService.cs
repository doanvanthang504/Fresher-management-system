using Global.Shared.Commons;
using Global.Shared.ViewModels.FeedbackViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IFeedbackService
    {
        public Task<FeedbackQuestionViewModel?> CreateFeedbackQuestionAsync(CreateFeedbackQuestionViewModel feedbackQuestion);
        public Task<FeedbackViewModel?> CreateFeedbackAsync(CreateFeedbackViewModel feedback);
        public Task<FeedbackResultViewModel?> CreateFeedbackResultAsync(CreateFeedbackResultViewModel feedbackResult);
        public Task<FeedbackAnswerViewModel?> CreateFeedbackAnswerAsync(CreateFeedbackAnswerViewModel feedbackAnswer);
        public Task<FeedbackQuestionViewModel?> GetFeedbackQuestionByIdAsync(Guid feedbackId);
        public Task<FeedbackViewModel?> GetFeedbackByIdAsync(Guid feedbackId);
        public Task<FeedbackResultViewModel?> GetFeedbackResultByIdAsync(Guid feedbackResultId);
        public Task<List<FeedbackResultViewModel>> GetAllResultOfFeedbackByFeedbackIdAsync(Guid feedbackId);
        public Task<FeedbackQuestionViewModel> UpdateFeedbackQuestionAsync(UpdateFeedbackQuestionViewModel feedbackQuestion);
        public Task<FeedbackAnswerViewModel> UpdateFeedbackAnswerAsync(UpdateFeedbackAnswerViewModel feedbackAnswer);
        public Task<FeedbackViewModel> UpdateFeedbackAsync(UpdateFeedbackViewModel feedback);
        public Task DeleteFeedbackAsync(Guid id);
        public Task DeleteFeedbackQuestionAsync(Guid id);
        public Task DeleteFeedbackAnswerAsync(Guid id);
        public Task<Pagination<FeedbackQuestionViewModel>> SearchFeedbackQuestionAsync(SearchFeedbackQuestionViewModel searchFeedbackQuestion);
        public Task<Pagination<FeedbackViewModel>> SearchFeedbackAsync(SearchFeedbackViewModel searchFeedback);
        public Task<Pagination<FeedbackResultViewModel>> SearchFeedbackResultAsync(SearchFeedbackResultViewModel searchFeedbackResult);
        public Task<List<FeedbackAnswerViewModel>> GetFeedbackAnswerByConstrainIdAsync(Type model, Guid feedbackId);
    }
}
