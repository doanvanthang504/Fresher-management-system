using FluentValidation;
using Global.Shared.ViewModels.FeedbackViewModels;
using System;

namespace WebAPI.Validations.FeedbacksValidation
{
    public class UpdateFeedbackQuestionViewModelValidation : AbstractValidator<UpdateFeedbackQuestionViewModel>
    {
        public UpdateFeedbackQuestionViewModelValidation()
        {
            RuleFor(x => x.FeedbackId).NotNull().NotEqual(Guid.Empty);
            RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Content).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(100);
        }
    }
}
