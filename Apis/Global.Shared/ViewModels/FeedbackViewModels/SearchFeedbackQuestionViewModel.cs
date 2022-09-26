using System;

namespace Global.Shared.ViewModels.FeedbackViewModels
{
    public class SearchFeedbackQuestionViewModel
    {
        public string? Title { get; set; }

        public Guid? FeedbackId { get;set; }

        public string? CreateBy { get;set; }

        public DateTimeOffset? CreationDate { get; set; }

        public int PageIndex { get; set; } = 0;

        public int PageSize { get; set; } = 10;
    }
}
