using FluentValidation;
using Global.Shared.ViewModels.AuthViewModels;

namespace WebAPI.Validations
{
    public class LoginRequestViewModelValidation : AbstractValidator<LoginRequestViewModel>
    {
        public LoginRequestViewModelValidation()
        {
            RuleFor(x => x.Username)
                            .NotEmpty()
                            .MinimumLength(5)
                            .MaximumLength(20)
                                .Unless(HasEmail)
                            .Must(username => false)
                                .When(HasBothUsernameAndEmail, ApplyConditionTo.CurrentValidator)
                                .WithMessage("Invalid request.");

            RuleFor(x => x.Email)
                            .MinimumLength(10)
                            .MaximumLength(50)
                            .EmailAddress()
                            .NotEmpty()
                                .Unless(HasUsername)
                            .Must(email => false)
                                .When(HasBothUsernameAndEmail, ApplyConditionTo.CurrentValidator)
                                .WithMessage("Invalid request.");

            RuleFor(x => x.Password).MinimumLength(5).MaximumLength(24).NotEmpty();
        }

        private bool HasUsername(LoginRequestViewModel req) => !string.IsNullOrEmpty(req.Username);
        private bool HasEmail(LoginRequestViewModel req) => !string.IsNullOrEmpty(req.Email);

        private bool HasBothUsernameAndEmail(LoginRequestViewModel req)
            => !string.IsNullOrEmpty(req.Username) && !string.IsNullOrEmpty(req.Email);
    }
}
