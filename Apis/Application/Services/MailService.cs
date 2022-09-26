using Application.Interfaces;
using Global.Shared.Helpers;
using Global.Shared.Settings;
using Global.Shared.Settings.Mail;
using System;
using System.Net;
using System.Net.Mail;
using System.Security;
using System.Threading.Tasks;

namespace Application.Services
{
    public class MailService : IMailService, IDisposable
    {
        private readonly SmtpClient _client;
        private readonly UserMailCredential _userMailCredential;

        public MailService(
            RootSetting rootSetting, IUserMailCredentialService userMailCredentialService)
        {
            var clientSetting = rootSetting.SmtpClientSetting!;
            _userMailCredential = userMailCredentialService.Credential;

            _client = CreateClient(clientSetting, _userMailCredential);
        }

        public async Task<MailSendingResult> SendAsync(MailMessage mail)
        {
            MailSendingResult result;
            mail.From = new MailAddress(_userMailCredential.Address!);

            try
            {
                await _client.SendMailAsync(mail);
                //mail.Body = mail.GetBody();
                result = new MailSendingResult(mail);
            }
            catch (Exception e)
            {
                result = new MailSendingResult(mail, true, e.Message);
            }

            return result;
        }

        private static SmtpClient CreateClient(SmtpClientSetting setting, UserMailCredential userMailCredential)
        {
            var client = new SmtpClient(setting.Host)
            {
                Port = setting.Port,
                DeliveryMethod = setting.DeliveryMethod,
                EnableSsl = setting.EnableSsl,
                UseDefaultCredentials = setting.UseDefaultCredentials
            };

            var credential = new NetworkCredential(
                userMailCredential.Address, ConvertToSecureString(userMailCredential.SecureString!));

            client.Credentials = credential;

            return client;
        }

        private static SecureString ConvertToSecureString(string password)
        {
            var secureString = new SecureString();

            foreach (var c in password)
                secureString.AppendChar(c);

            secureString.MakeReadOnly();

            return secureString;
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}