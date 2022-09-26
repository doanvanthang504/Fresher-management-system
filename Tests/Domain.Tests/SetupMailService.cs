using Global.Shared.Settings.Mail;
using Microsoft.Extensions.Configuration;

namespace Domain.Tests
{
    public class SetupMailService
    {
        public static UserMailCredential CreateUserMailCredential(IConfiguration configuration)
        {
            var userMailCredential = new UserMailCredential
            {
                Address = configuration
                                .GetRequiredSection(nameof(UserMailCredential))
                                .GetRequiredSection(nameof(UserMailCredential.Address))
                                .Value,
                SecureString = configuration
                                .GetRequiredSection(nameof(UserMailCredential))
                                .GetRequiredSection(nameof(UserMailCredential.SecureString))
                                .Value
            };

            return userMailCredential;
        }
    }
}
