using System;

namespace Global.Shared.ViewModels.ModuleResultViewModels
{
    public class CreateModuleResultViewModel
    {
        public Guid FresherId { get; set; }

        public Guid ClassId { get; set; }

        public string ModuleName { get; set; }

        public double? AssignmentAvgScore { get; set; } = 0;

        public double? QuizzAvgScore { get; set; } = 0;

        public double? FinalAuditScore { get; set; } = 0;

        public double? FinalMark { get; set; } = 0;
    }
}
