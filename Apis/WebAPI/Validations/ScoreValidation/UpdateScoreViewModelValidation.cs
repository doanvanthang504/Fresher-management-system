using FluentValidation;
using Global.Shared.ViewModels.ScoreViewModels;

namespace WebAPI.Validations.ScoreValidation
{
    public class UpdateScoreViewModelValidation : AbstractValidator<UpdateScoreViewModel>
    {
        public UpdateScoreViewModelValidation()
        {
            RuleFor(e => e.TypeScore).IsInEnum().NotNull();

            RuleFor(e => e.ModuleScore).NotEmpty().NotNull()
                                       .GreaterThanOrEqualTo(0)
                                       .LessThanOrEqualTo(10);

        }
    }
}
