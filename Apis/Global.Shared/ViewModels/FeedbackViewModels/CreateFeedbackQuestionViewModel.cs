using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Global.Shared.ViewModels.FeedbackViewModels
{
    public class CreateFeedbackQuestionViewModel
    {
        public Guid FeedbackId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Description { get; set; }
        public FeedbackQuestionType QuestionType { get; set; }
        public virtual List<CreateFeedbackAnswerViewModel> FeedbackAnswers { get; set; }
    }
}
