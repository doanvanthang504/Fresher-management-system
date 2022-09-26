using System;

namespace Global.Shared.ViewModels.PlanViewModels
{
    public class PlanGetViewModel
    {
        public Guid Id { get; set; }

        public string? CourseName { get; set; }

        public string? CourseCode { get; set; }

        public bool IsDeleted { get; set; }
    }
}
