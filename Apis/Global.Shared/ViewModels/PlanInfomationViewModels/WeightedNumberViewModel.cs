using System;

namespace Global.Shared.ViewModels.PlanInfomationViewModels
{
    public class WeightedNumberViewModel
    {
        public Guid? ClassId { get; set; }
        public string? ModuleName { get; set; }
        public double? Duration { get; set; }
        public double WeightedNumberQuizz { get; set; }
        public double WeightedNumberAssignment { get; set; }
        public double WeightedNumberFinal { get; set; }
    }
}
