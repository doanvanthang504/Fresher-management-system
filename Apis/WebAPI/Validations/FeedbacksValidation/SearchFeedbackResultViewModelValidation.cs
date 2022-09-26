using FluentValidation;
using Global.Shared.ViewModels.FeedbackViewModels;

namespace WebAPI.Validations.FeedbacksValidation
{
    public class SearchFeedbackResultViewModelValidation : AbstractValidator<SearchFeedbackResultViewModel>
    {
        public SearchFeedbackResultViewModelValidation()
        {
            RuleFor(x => x.PageIndex).GreaterThanOrEqualTo(0);
            RuleFor(x => x.PageSize).GreaterThanOrEqualTo(0);
        }
    }
}
