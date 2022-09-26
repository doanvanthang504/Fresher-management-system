using System.Net.Mail;

namespace Global.Shared.Settings.Mail
{
    public class SmtpClientSetting
    {
        public string? Host { get; set; }

        public int Port { get; set; }

        public SmtpDeliveryMethod DeliveryMethod { get; set; }

        public bool EnableSsl { get; set; }

        public bool UseDefaultCredentials { get; set; }

    }
}