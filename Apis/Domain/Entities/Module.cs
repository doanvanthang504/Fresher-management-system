using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Module : BaseEntity
    {
        public string? ModuleName { get; set; }

        public Guid PlanId { get; set; }

        public Plan? Plan { get; set; }

        public int Order { get; set; }
        public double DurationTotal { get; set; }

        public double WeightedNumberQuizz { get; set; }

        public double WeightedNumberAssignment { get; set; }

        public double WeightedNumberFinal { get; set; }

        public string? Mentor { get; set; }

        public ICollection<Topic>? Topics { get; set; }
    }
}
