using System;

namespace Global.Shared.ViewModels.FeedbackViewModels
{
    public class CreateFeedbackAnswerViewModel
    {
        public string? Content { get; set; }
        public Guid QuestionId { get; set; }
    }
}
