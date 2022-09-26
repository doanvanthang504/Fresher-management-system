using System;

namespace Global.Shared.ViewModels.PlanInfomationViewModels
{
    public class PlanInformationViewModel
    {
        public Guid Id { get; set; }

        public Guid ClassId { get; set; }

        public string? PlanName { get; set; }

        public string? ModuleName { get; set; }

        public string? TopicName { get; set; }

        public string? Pic { get; set; }

        public DateOnly? StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public double? Duration { get; set; }

        public double WeightedNumberQuizz { get; set; }

        public double WeightedNumberAssignment { get; set; }

        public double WeightedNumberFinal { get; set; }

        public string? NoteDetail { get; set; }

        public bool IsDeleted { get; set; }
    }
}
