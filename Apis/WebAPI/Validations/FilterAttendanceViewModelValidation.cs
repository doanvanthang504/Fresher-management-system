using FluentValidation;
using Global.Shared.ViewModels;
using Global.Shared.ViewModels.AttendancesViewModels;

namespace WebAPI.Validations
{
    public class FilterAttendanceViewModelValidation : AbstractValidator<FilterAttendanceViewModel>
    {
        public FilterAttendanceViewModelValidation()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Month).NotEmpty().InclusiveBetween(1, 12);
            RuleFor(x => x.Year).NotEmpty().InclusiveBetween(1000, 9999);
        }
    }
}
