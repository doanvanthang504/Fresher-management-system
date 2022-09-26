using System;

namespace Global.Shared.ViewModels.FeedbackViewModels
{
    public class FeedbackAnswerViewModel
    {
        public Guid AnswerId { get; set; }
        public Guid QuestionId { get; set; }
        public string Content { get; set; }
        //public Guid? CreatedBy { get; set; }
        //public DateTime CreationDate { get; set; }
        //public DateTime? ModificationDate { get; set; }
        //public DateTime? DeletionDate { get; set; }
    }
}
