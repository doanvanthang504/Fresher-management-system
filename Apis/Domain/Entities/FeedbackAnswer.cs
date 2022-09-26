using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class FeedbackAnswer: BaseEntity
    {
        public Guid QuestionId { get; set; }
        public string Content { get; set; }
        public virtual FeedbackQuestion Question { get; set; }
    }                  
}
