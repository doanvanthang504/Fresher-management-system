using FluentValidation;
using Global.Shared.Extensions;
using Global.Shared.ViewModels;
using System;

namespace WebAPI.Validations
{
    public class UpdateClassFresherViewModelValidation : AbstractValidator<UpdateClassFresherInfoViewModel>
    {
        public UpdateClassFresherViewModelValidation()
        {
            RuleFor(x => x.ClassCode).NotEmpty()
                .WithMessage("{PropertyName} should be not empty. NEVER!");             
            RuleFor(x => x.NameAdmin1).NotEmpty()
                .WithMessage("Class must has {PropertyName}");
            RuleFor(x => x.CLassName).NotEmpty();
            RuleFor(x => x.NameAdmin2).NotEmpty()
                .WithMessage("Class must has at least 2 admins");
            RuleFor(x => x.NameTrainer1).NotNull();
            RuleFor(x => x.NameTrainer2).NotNull()
                .WithMessage("Class must has least 2 trainers");
            RuleFor(x => x.EmailAdmin1).NotEmpty();
            RuleFor(x => x.EmailAdmin2).NotEmpty();
            RuleFor(x => x.EmailTrainer1).NotEmpty();
            RuleFor(x => x.EmailTrainer2).NotEmpty();
            RuleFor(x => x.EndDate)
                .Must((x, endate) => endate > x.StartDate.AddMonths(2))
                .WithMessage("End date must geater than start date 2 months").NotEmpty()
                .WithMessage("Class must has start date");
            RuleFor(x => x.StartDate)
                .Must((x, startdate) => startdate > DateTime.Now.ToDateOnly())
                .WithMessage("Start date must less than current date")
                .NotEmpty()
                .WithMessage("Class must has end date");
            RuleFor(x => x.Location).NotEmpty().WithMessage("Class must has location");

        }       
    }
}
