using System.Net.Mail;

namespace Global.Shared.Helpers
{
    public class MailSendingResult
    {
        public MailSendingResult(MailMessage mail, bool isError = false, string? errorMessage = null)
        {
            Mail = mail;
            IsError = isError;
            ErrorMessage = errorMessage;
        }

        public MailMessage Mail { get; set; }

        public bool IsError { get; }

        public string? ErrorMessage { get; }
    }
}
