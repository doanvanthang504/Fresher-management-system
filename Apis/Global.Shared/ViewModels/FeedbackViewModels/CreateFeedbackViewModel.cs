using System;
using System.Collections.Generic;

namespace Global.Shared.ViewModels.FeedbackViewModels
{
    public class CreateFeedbackViewModel
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string Description { get; set; }

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset EndDate { get; set; }

        public ICollection<CreateFeedbackQuestionViewModel> FeedBackQuestions { get; set; }
    }
}
