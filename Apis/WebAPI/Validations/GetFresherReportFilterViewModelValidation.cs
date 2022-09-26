using FluentValidation;
using Global.Shared.ViewModels.ReportsViewModels;

namespace WebAPI.Validations
{
    public class GetFresherReportFilterViewModelValidation: AbstractValidator<GetFresherReportFilterViewModel>
    {
        public GetFresherReportFilterViewModelValidation()
        {
            RuleFor(e => e.Month).LessThanOrEqualTo(12)
                                 .GreaterThanOrEqualTo(1);
            RuleFor(e => e.Year).GreaterThanOrEqualTo(1);
        }
    }
}
