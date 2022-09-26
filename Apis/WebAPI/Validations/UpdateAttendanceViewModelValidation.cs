using FluentValidation;
using Global.Shared.ViewModels.AttendancesViewModels;

namespace WebAPI.Validations
{
    public class UpdateAttendanceViewModelValidation : AbstractValidator<UpdateAttendanceViewModel>
    {
        public UpdateAttendanceViewModelValidation()
        {
            RuleFor(x => x.Status1).NotNull();
            RuleFor(x => x.Status2).NotNull();
        }
    }
}
