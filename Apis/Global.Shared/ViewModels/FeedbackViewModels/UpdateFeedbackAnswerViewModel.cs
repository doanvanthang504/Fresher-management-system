using Domain.Enums;
using System;

namespace Global.Shared.ViewModels.FeedbackViewModels
{
    public class UpdateFeedbackAnswerViewModel
    {
        public Guid AnswerId { get; set; }
        public Guid QuestionId { get; set; }
        public string Content { get; set; }
        //public UpdateStatuses Status { get; set; }

    }
}
