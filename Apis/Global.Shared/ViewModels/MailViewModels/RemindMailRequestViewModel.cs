using System;

namespace Global.Shared.ViewModels.MailViewModels
{
    public class RemindMailRequestViewModel
    {
        public string? ToAddresses { get; set; }

        public string? Subject { get; set; }

        public string? Body { get; set; }

        public DateTimeOffset? EventTime { get; set; }
    }
}