using FluentValidation;
using Global.Shared.ViewModels.FeedbackViewModels;

namespace WebAPI.Validations.FeedbacksValidation
{
    public class CreateFeedbackViewModelValidation: AbstractValidator<CreateFeedbackViewModel>
    {
        public CreateFeedbackViewModelValidation()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Content).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(100);
            RuleFor(x => x.StartDate).NotEmpty();
            RuleFor(x => x.EndDate).NotEmpty();
            RuleFor(x => x.StartDate).LessThanOrEqualTo(x => x.EndDate);
            RuleFor(x => x.EndDate).GreaterThanOrEqualTo(x => x.StartDate);
        }
    }
}
