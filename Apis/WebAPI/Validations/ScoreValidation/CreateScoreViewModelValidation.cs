using FluentValidation;
using Global.Shared.ViewModels.ScoreViewModels;

namespace WebAPI.Validations.ScoreValidation
{
    public class CreateScoreViewModelValidation : AbstractValidator<CreateScoreViewModel>
    {
        public CreateScoreViewModelValidation()
        {
            RuleFor(e => e.TypeScore).IsInEnum();

            //score 0 - 10
            RuleFor(e => e.ModuleScore).NotEmpty()
                .GreaterThanOrEqualTo(0).LessThanOrEqualTo(10);

            RuleFor(e => e.ModuleName).NotNull();
            RuleFor(e => e.ClassId).NotNull();
            RuleFor(e => e.FresherId).NotNull();
        }
    }
}
