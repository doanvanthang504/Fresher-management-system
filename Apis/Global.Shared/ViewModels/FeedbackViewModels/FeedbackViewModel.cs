using System;

namespace Global.Shared.ViewModels.FeedbackViewModels
{
    public class FeedbackViewModel
    {
        public Guid FeedbackId { get; set; }

        public Guid? CreatedBy { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Description { get; set; }

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset EndDate { get; set; }

        public DateTimeOffset CreationDate { get; set; }

        public DateTimeOffset? ModificationDate { get; set; }

        public DateTimeOffset? DeletionDate { get; set; }
    }
}
