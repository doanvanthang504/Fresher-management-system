using Global.Shared.ViewModels.TopicViewModels;
using System;
using System.Collections.Generic;

namespace Global.Shared.ViewModels.ModuleViewModels
{
    public class ModuleViewModel
    {
        public Guid Id { get; set; }

        public string ModuleName { get; set; }

        public Guid PlanId { get; set; }

        public int Order { get; set; }

        public double WeightedNumberQuizz { get; set; }

        public double WeightedNumberAssignment { get; set; }

        public double WeightedNumberFinal { get; set; }

        public double DurationTotal { get; set; }

        public string? Mentor { get; set; }

        public ICollection<TopicViewModel>? Topics { get; set; }
    }
}
