using Domain.Enums;
using System;

namespace Global.Shared.ViewModels.ModuleResultViewModels
{
    public class ModuleResultViewModel
    {
        public Guid FresherId { get; set; }

        public string FresherFirstName { get; set; }

        public string FresherLastName { get; set; }
        public string FresherAccount { get; set; }
        public string ModuleName { get; set; }

        public double? AssignmentAvgScore { get; set; }

        public double? QuizzAvgScore { get; set; }

        public double? FinalAuditScore { get; set; }

        public double? FinalMark { get; set; }
        public string Rank { get; set; }

    }
}
