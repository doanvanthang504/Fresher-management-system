using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class FeedbackQuestion : BaseEntity
    {
        public Guid FeedbackId { get; set; }
       
        public string? Title { get; set; }
        
        public string? Content { get; set; }
        
        public string? Description { get; set; }
        
        public FeedbackQuestionType QuestionType { get; set; }
        
        public virtual Feedback? Feedback { get; set; }
        
        public virtual ICollection<FeedbackResult>? FeedbackResults { get; set; }
        
        public virtual ICollection<FeedbackAnswer>? FeedbackAnswers { get; set; }

    }
}
