using Global.Shared.Settings.Mail;

namespace Application.Interfaces
{
    public interface IUserMailCredentialService
    {
        UserMailCredential Credential { get; }
    }
}