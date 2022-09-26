using FluentValidation;
using Global.Shared.ViewModels;

namespace WebAPI.Validations
{
    public class FresherViewModelValidation : AbstractValidator<FresherViewModel>
    {
        public FresherViewModelValidation()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Fresher must has email");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Fresher must has phone number");
            RuleFor(x => x.English).NotEmpty();
            RuleFor(x => x.GPA).NotEmpty();
            RuleFor(x => x.RRCode).NotEmpty().WithMessage("Fresher must has RR code");
            RuleFor(x => x.Skill).NotEmpty().WithMessage("Fresher must has skill");
            RuleFor(x => x.AccountName).NotEmpty().WithMessage("Fresher must has account name");
            RuleFor(x => x.ContactType).NotEmpty();
            RuleFor(x => x.Graduation).NotEmpty();
            RuleFor(x => x.Major).NotEmpty();
            RuleFor(x => x.University).NotEmpty();
        }
    }
}
