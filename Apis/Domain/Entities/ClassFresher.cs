using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class ClassFresher : BaseEntity
    {
        public string? ClassCode { get; set; }

        public string? Location { get; set; }

        public string? ClassName { get; set; }

        public string? RRCode { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public string? NameAdmin1 { get; set; }

        public string? EmailAdmin1 { get; set; }

        public string? NameAdmin2 { get; set; }

        public string? EmailAdmin2 { get; set; }

        public string? NameAdmin3 { get; set; }

        public string? EmailAdmin3 { get; set; }

        public string? NameTrainer1 { get; set; }

        public string? EmailTrainer1 { get; set; }

        public string? NameTrainer2 { get; set; }

        public string? EmailTrainer2 { get; set; }

        public string? NameTrainer3 { get; set; }

        public string? EmailTrainer3 { get; set; }

        public bool IsDone { get; set; }
        public Guid PlanId { get; set; }

        public double Budget { get; set; }

        public ICollection<Fresher> Freshers { get; set; }
    }
}
