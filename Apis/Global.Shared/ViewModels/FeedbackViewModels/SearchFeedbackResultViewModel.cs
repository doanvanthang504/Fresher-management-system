using System;

namespace Global.Shared.ViewModels.FeedbackViewModels
{
    public class SearchFeedbackResultViewModel
    {
        public Guid? QuestionId { get; set; }

        public string AccountName { get; set; }

        public DateTimeOffset? CreationDate { get; set; }

        public int PageIndex { get; set; } = 0;

        public int PageSize { get; set; } = 10;
    }
}
