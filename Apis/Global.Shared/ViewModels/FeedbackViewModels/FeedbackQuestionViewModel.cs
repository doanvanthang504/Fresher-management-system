using Domain.Enums;
using System;

namespace Global.Shared.ViewModels.FeedbackViewModels
{
    public class FeedbackQuestionViewModel
    {
        public Guid QuestionId { get; set; }
        public Guid FeedbackId { get; set; }
        public Guid? CreatedBy { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public FeedbackQuestionType QuestionType { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public DateTimeOffset? ModificationDate { get; set; }
        public DateTimeOffset? DeletionDate { get; set; }
    }
}
