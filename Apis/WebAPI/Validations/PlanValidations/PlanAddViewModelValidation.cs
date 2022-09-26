using FluentValidation;
using Global.Shared.ViewModels.PlanViewModels;
using System.Text.RegularExpressions;

namespace WebAPI.Validations.PlanValidations
{
    public class PlanAddViewModelValidation
                    :AbstractValidator<PlanAddViewModel>
    {
        public PlanAddViewModelValidation()
        {
            RuleFor(x => x.CourseName).NotEmpty()
                                      .WithMessage("{PropertyName} not empty!");
            RuleFor(x => x.CourseName).NotNull()
                                      .WithMessage("{PropertyName} not null!");
            RuleFor(x=>x.CourseCode).NotNull()
                                    .WithMessage("{PropertyName} not empty!");
            RuleFor(x => x.CourseCode).NotEmpty()
                                    .WithMessage("{PropertyName} not empty!");
            RuleFor(x => x.CourseCode).Must(CheckSpace)
                                      .WithMessage("{PropertyName} Invalid!");
        }
        private bool CheckSpace(string? value)
        {
            ///not allow space in string
            var regex = new Regex(@"^\w[a-zA-Z@#0-9.]*$");
            if (value == null)
            {
                return false;
            }
            var inValid = regex.IsMatch(value);
            if(!inValid)
            {
                return false;
            }
            return true;
        }
    }
}
