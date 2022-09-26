using System;

namespace Global.Shared.ViewModels.FeedbackViewModels
{
    public class UpdateFeedbackViewModel
    {
        public Guid FeedbackId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Description { get; set; }

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset EndDate { get; set; }
    }
}
