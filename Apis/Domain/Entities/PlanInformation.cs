using System;

namespace Domain.Entities
{
    public class PlanInformation : BaseEntity
    {
        public Guid? ClassId { get; set; }

        public string? PlanName { get; set; }

        public string? ModuleName { get; set; }

        public string? TopicName { get; set; }

        public Guid TopicId { get; set; }

        public string? Pic { get; set; }

        public DateOnly? StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public double? Duration { get; set; }

        public double WeightedNumberQuizz { get; set; }

        public double WeightedNumberAssignment { get; set; }

        public double WeightedNumberFinal { get; set; }

        public double WeightedNumberPractice { get; set; }

        public double WeightedNumberAudit { get; set; }

        public string? NoteDetail { get; set; }
    }
}
