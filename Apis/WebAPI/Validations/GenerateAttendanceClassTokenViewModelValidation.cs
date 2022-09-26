using FluentValidation;
using Global.Shared.ViewModels.AttendancesViewModels;

namespace WebAPI.Validations
{
    public class GenerateAttendanceClassTokenViewModelValidation : AbstractValidator<GenerateAttendanceClassTokenViewModel>
    {
        public GenerateAttendanceClassTokenViewModelValidation()
        {
            RuleFor(x => x.ClassId).NotEmpty();
            RuleFor(x => x.ExpiredLinkMinutes).NotNull().InclusiveBetween(1, 60);
            RuleFor(x => x.TypeAttendance).NotNull().InclusiveBetween(1, 2);
        }
    }
}


