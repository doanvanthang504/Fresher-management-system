using Domain.Enums;
using FluentValidation;
using Global.Shared.ViewModels.ReportsViewModels;

namespace WebAPI.Validations
{
    public class UpdateFresherReportViewModelValidation: AbstractValidator<UpdateFresherReportViewModel>
    {
        public UpdateFresherReportViewModelValidation()
        {
            RuleFor(e => e.Status).IsInEnum();
            RuleFor(e => e.Note).NotEmpty();
        }
    }
}
