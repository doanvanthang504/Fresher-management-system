using Application.Interfaces;
using Global.Shared.Settings.Mail;
using Microsoft.Extensions.Configuration;
using System;

namespace WebAPI.Services
{
    public class UserMailCredentialService : IUserMailCredentialService
    {
        public UserMailCredentialService(IConfiguration configuration)
        {
            Credential = new UserMailCredential();

            configuration.GetSection(nameof(UserMailCredential)).Bind(Credential);

            if (string.IsNullOrWhiteSpace(Credential.Address) || string.IsNullOrWhiteSpace(Credential.SecureString))
                throw new ArgumentNullException("Address/SecureString is empty.");
        }

        public UserMailCredential Credential { get; }
    }
}