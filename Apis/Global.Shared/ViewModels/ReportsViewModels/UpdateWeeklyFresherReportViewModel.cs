using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Global.Shared.ViewModels.ReportsViewModels
{
    public class UpdateWeeklyFresherReportViewModel
    {
        [Required]
        public ReportStatusEnum Status { get; set; }

        [Required]
        public string Note { get; set; }
    }
}
