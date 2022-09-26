using FluentValidation;
using Global.Shared.ViewModels.ChemicalsViewModels;

namespace WebAPI.Validations
{
    public class CreateChemicalViewModelValidation : AbstractValidator<CreateChemicalViewModel>
    {
        public CreateChemicalViewModelValidation()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        }
    }
}
