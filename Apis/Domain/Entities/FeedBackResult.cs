using Domain.Enums;
using System;

namespace Domain.Entities
{
    public class FeedbackResult : BaseEntity
    {
        public Guid FeedbackId { get; set; }

        public Guid QuestionId { get; set; }
        
        public Guid AccountFresherId { get; set; }
        
        public string AccountName { get; set; }
        
        public string QuestionTitle { get; set; }
        
        public FeedbackQuestionType QuestionType { get; set; }
        
        public string Fullname { get; set; }
        
        public string Content { get; set; }
        
        public string Note { get; set; }
        
        public virtual Feedback? Feedback { get; set; }
        
        public virtual FeedbackQuestion? FeedbackQuestion { get; set; }
    }
}
