using FluentValidation;
using Global.Shared.ViewModels.PlanInfomationViewModels;
using System.Text.RegularExpressions;

namespace WebAPI.Validations.PlanValidations
{
    public class ChoosePlanForClassViewModelValidation : AbstractValidator<ChoosePlanForClassViewModel>
    {
        public ChoosePlanForClassViewModelValidation()
        {
            RuleFor(x => x.StartDate).NotEmpty()
                                   .WithMessage("{PropertyName} not empty!");
            RuleFor(x => x.StartDate).NotNull()
                                   .WithMessage("{PropertyName} not null!");
            RuleFor(x => x.StartDate).Must(CheckDateTime)
                                   .WithMessage("Invalid {PropertyName}!");
        }
        private bool CheckDateTime(string? date)
        {
            //Regex format date dd/MM/yyyy
            var regex = new Regex(@"(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$");
            if (date != null)
            {
                bool isValid = regex.IsMatch(date.Trim());
                if (!isValid)
                {
                    return false;
                }
                return true;
            }
            return false;
        }
    }
}
