using FluentValidation;
using Global.Shared.ViewModels.TopicViewModels;

namespace WebAPI.Validations.PlanValidations
{
    public class CreateTopicViewModelValidation 
                        : AbstractValidator<CreateTopicViewModel>
    {
        public CreateTopicViewModelValidation()
        {
            RuleFor(x => x.Name).NotNull()
                                .WithMessage("{PropertyName} not null!");
            RuleFor(x => x.Order).GreaterThanOrEqualTo(0)
                                 .WithMessage("{PropertyName} greather than or equal 0!");
            RuleFor(x => x.Duration).GreaterThanOrEqualTo(0)
                                 .WithMessage("{PropertyName} greather than or equal 0!");
        }
    }
}
