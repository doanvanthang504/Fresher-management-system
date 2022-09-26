using FluentValidation;
using Global.Shared.ViewModels.AttendancesViewModels;

namespace WebAPI.Validations
{
    public class GenerateAttendanceTokenViewModelValidation : AbstractValidator<GenerateAttendanceTokenViewModel>
    {
        public GenerateAttendanceTokenViewModelValidation()
        {
            RuleFor(x => x.FresherId).NotEmpty();
            RuleFor(x => x.ExpiredLinkMinutes).NotNull().InclusiveBetween(1, 60);
            RuleFor(x => x.TypeAttendance).NotNull().InclusiveBetween(1, 2);
        }
    }
}


