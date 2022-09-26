using Domain.Enums;
using System;

namespace Global.Shared.ViewModels.FeedbackViewModels
{
    public class CreateFeedbackResultViewModel
    {
        public Guid FeedBackId { get; set; }

        public Guid QuestionId { get; set; }

        public Guid AccountFresherId { get; set; }
        public string QuestionTitle { get; set; }

        public FeedbackQuestionType QuestionType { get; set; }
        public string AccountName { get; set; }

        public string Fullname { get; set; }

        public string Content { get; set; }

        public string Note { get; set; }
    }
}
