using FluentValidation;
using Global.Shared.ViewModels.FeedbackViewModels;

namespace WebAPI.Validations.FeedbacksValidation
{
    public class CreateFeedbackQuestionViewModelValidation :AbstractValidator<CreateFeedbackQuestionViewModel>
    {
        public CreateFeedbackQuestionViewModelValidation()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Content).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(100);
        }
    }
}
