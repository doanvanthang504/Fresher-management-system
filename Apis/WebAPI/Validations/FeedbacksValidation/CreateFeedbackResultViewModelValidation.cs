using FluentValidation;
using Global.Shared.ViewModels.FeedbackViewModels;

namespace WebAPI.Validations.FeedbacksValidation
{
    public class CreateFeedbackResultViewModelValidation: AbstractValidator<CreateFeedbackResultViewModel>
    {
        public CreateFeedbackResultViewModelValidation()
        {
            RuleFor(x => x.FeedBackId).NotEmpty();
            RuleFor(x => x.QuestionId).NotEmpty();
            RuleFor(x => x.AccountFresherId).NotEmpty();
            RuleFor(x => x.AccountName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Fullname).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Content).NotEmpty().MaximumLength(100);
        }
    }
}
