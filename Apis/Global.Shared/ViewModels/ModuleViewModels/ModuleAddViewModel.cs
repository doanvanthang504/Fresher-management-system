using System;

namespace Global.Shared.ViewModels.ModuleViewModels
{
    public class ModuleAddViewModel
    {
        public string ModuleName { get; set; }

        public Guid PlanId { get; set; }

        public int Order { get; set; }

        public double WeightedNumberQuizz { get; set; }

        public double WeightedNumberAssignment { get; set; }

        public double WeightedNumberFinal { get; set; }

        public double DurationTotal { get; set; }

        public string? Mentor { get; set; }
    }
}
