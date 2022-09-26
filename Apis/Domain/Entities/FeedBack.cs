using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Feedback : BaseEntity
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string Description { get; set; }

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset EndDate { get; set; }

        public ICollection<FeedbackQuestion>? FeedbackQuestions { get; set; }

        public ICollection<FeedbackResult>? FeedbackResults { get; set; }
    }
}
