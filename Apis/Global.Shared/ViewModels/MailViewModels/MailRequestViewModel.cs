namespace Global.Shared.ViewModels.MailViewModels
{
    public class MailRequestViewModel
    {
        public string? ToAddresses { get; set; }

        public string? CCAddresses { get; set; }

        public string? Subject { get; set; }

        public string? Body { get; set; }
    }
}