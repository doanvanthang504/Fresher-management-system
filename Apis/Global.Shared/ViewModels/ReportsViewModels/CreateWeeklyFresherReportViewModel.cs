using System.ComponentModel.DataAnnotations;

namespace Global.Shared.ViewModels.ReportsViewModels
{
    public class CreateWeeklyFresherReportViewModel
    {
        [Required]
        public string CourseCode { get; set; }

        [Required]
        public string FromDate { get; set; }
    }
}
