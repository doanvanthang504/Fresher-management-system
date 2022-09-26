using FluentValidation;
using Global.Shared.ViewModels.AttendancesViewModels;

namespace WebAPI.Validations
{
    public class CreateAttendanceViewModelValidation : AbstractValidator<CreateAttendanceViewModel>
    {
        public CreateAttendanceViewModelValidation()
        {
            RuleFor(x => x.ClassId).NotEmpty();
        }
    }
}

