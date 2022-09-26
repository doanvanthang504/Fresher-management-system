using Domain.Enums;

namespace Global.Shared.ViewModels.ReportsViewModels
{
    public class UpdateFresherReportViewModel
    {
        public StatusFresherEnum? Status { get; set; }

        public string Note { get; set; }
    }
}
