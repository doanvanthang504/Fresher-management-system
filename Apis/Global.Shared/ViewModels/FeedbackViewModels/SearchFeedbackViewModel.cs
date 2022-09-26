using System;

namespace Global.Shared.ViewModels.FeedbackViewModels
{
    public class SearchFeedbackViewModel
    {
        public string? Title { get; set; }

        public string? CreateBy { get; set; }

        public DateTimeOffset? CreationDate { get; set; }

        public int PageIndex { get; set; } = 0;

        public int PageSize { get; set; } = 10;
    }
}
