using FluentValidation;
using Global.Shared.ViewModels.ImportViewModels;

namespace WebAPI.Validations
{
    public class FreshersIFromImportExcelFileViewModelConfiguration : AbstractValidator<FresherImportViewModel>
    {
        public FreshersIFromImportExcelFileViewModelConfiguration()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Fresher must has email");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email is not valid");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Fresher must has phone number");
            RuleFor(x => x.GPA).NotEmpty().WithMessage("Fresher must has GPA");
            RuleFor(x => x.RRCode).NotEmpty().WithMessage("Fresher must has RR code");
            RuleFor(x => x.Skill).NotEmpty().WithMessage("Fresher must has skill");
            RuleFor(x => x.AccountName).NotEmpty().WithMessage("Fresher must has account name");
            RuleFor(x => x.ContractType).NotEmpty().WithMessage("Fresher must has contract type");

            RuleFor(x => x.Graduation).NotEmpty().WithMessage("Fresher must has graduation");
            RuleFor(x => x.Graduation).GreaterThan(2099).WithMessage("Graduation year is not valid");
            RuleFor(x => x.Graduation).LessThan(1979).WithMessage("Graduation year is not valid");

            RuleFor(x => x.Major).NotEmpty().WithMessage("Fresher must has major");
            RuleFor(x => x.University).NotEmpty().WithMessage("Fresher must has university");
            RuleFor(x => x.Salary).NotEmpty().WithMessage("Fresher must has salary");


        }

    }
}
