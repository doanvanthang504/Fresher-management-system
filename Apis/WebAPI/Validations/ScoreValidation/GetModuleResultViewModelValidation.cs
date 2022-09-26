using FluentValidation;
using Global.Shared.ViewModels.ModuleResultViewModels;

namespace WebAPI.Validations.ScoreValidation
{
    public class GetModuleResultViewModelValidation : AbstractValidator<GetModuleResultViewModel>
    {
        public GetModuleResultViewModelValidation()
        {
            RuleFor(e => e.ClassId).NotNull();
            RuleFor(e => e.ModuleName).NotNull();
        }
    }
}
