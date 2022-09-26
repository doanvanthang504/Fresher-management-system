using FluentValidation;
using Global.Shared.ViewModels;

namespace WebAPI.Validations
{
    public class ClassFresherFromImportExcelFileViewModelConfiguration : AbstractValidator<ClassFresherViewModel>
    {
        public ClassFresherFromImportExcelFileViewModelConfiguration()
        {
            RuleFor(x => x.RRCode).NotEmpty()
                .WithMessage("{PropertyName} should be not empty. NEVER!");
        }        
    }
}
