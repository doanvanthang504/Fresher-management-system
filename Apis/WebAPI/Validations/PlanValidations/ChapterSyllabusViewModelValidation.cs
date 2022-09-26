using FluentValidation;
using Global.Shared.ViewModels.ChapterSyllabusViewModels;

namespace WebAPI.Validations.PlanValidations
{
    public class ChapterSyllabusViewModelValidation : AbstractValidator<ChapterSyllabusViewModel>
    {
        public ChapterSyllabusViewModelValidation()
        {
            RuleFor(x => x.Name).MinimumLength(0);
        }
       
    }
}
