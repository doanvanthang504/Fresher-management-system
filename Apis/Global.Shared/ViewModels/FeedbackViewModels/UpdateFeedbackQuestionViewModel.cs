using Domain.Enums;
using System;

namespace Global.Shared.ViewModels.FeedbackViewModels
{
    public class UpdateFeedbackQuestionViewModel
    {
        public Guid QuestionId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Description { get; set; }

        public Guid FeedbackId { get; set; }
    }
}
