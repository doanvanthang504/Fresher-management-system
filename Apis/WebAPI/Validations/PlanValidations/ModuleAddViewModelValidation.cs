using FluentValidation;
using Global.Shared.ViewModels.ModuleViewModels;

namespace WebAPI.Validations.PlanValidations
{
    public class ModuleAddViewModelValidation
                      : AbstractValidator<ModuleAddViewModel>
    {
        public ModuleAddViewModelValidation()
        {
            RuleFor(x => x.ModuleName).NotNull()
                                      .NotEmpty()
                                      .WithMessage("{PropertyName} is not null!");
            RuleFor(x => x.ModuleName).MaximumLength(100)
                                      .WithMessage("{PropertyName} Invalid!");
            RuleFor(x => x.Order).GreaterThan(0)
                                 .WithMessage("{PropertyName} mus greater 0!");
            RuleFor(x => x.DurationTotal).GreaterThanOrEqualTo(0)
                                         .WithMessage("{PropertyName} mus greater than or equal 0!");
            RuleFor(x => x.Mentor).MaximumLength(20)
                                  .WithMessage("{PropertyName} Invalid!");
            RuleFor(x => x.WeightedNumberAssignment).NotEmpty()
                                                    .NotNull()
                                                    .WithMessage("{PropertyName} required!");
            RuleFor(x => x.WeightedNumberQuizz).NotEmpty()
                                               .NotNull()
                                               .WithMessage("{PropertyName} required!");
            RuleFor(x => x.WeightedNumberFinal).NotEmpty()
                                               .NotNull()
                                               .WithMessage("{PropertyName} required!");
        }
    }
}
