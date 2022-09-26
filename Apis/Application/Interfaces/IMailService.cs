using System.Net.Mail;
using System.Threading.Tasks;
using Global.Shared.Helpers;

namespace Application.Interfaces
{
    public interface IMailService
    {
        Task<MailSendingResult> SendAsync(MailMessage mail);
    }
}