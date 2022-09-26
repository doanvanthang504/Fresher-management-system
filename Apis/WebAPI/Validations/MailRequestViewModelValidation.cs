using Domain.Enums;
using FluentValidation;
using Global.Shared.ViewModels.MailViewModels;

namespace WebAPI.Validations
{
    public class MailRequestViewModelValidation : AbstractValidator<MailRequestViewModel>
    {
        public MailRequestViewModelValidation()
        {
            // Should check for email regex for each email
            RuleFor(x => x.ToAddresses).NotEmpty();
        }
    }
}
