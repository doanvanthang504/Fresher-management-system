using System;
using System.Collections.Generic;

namespace Global.Shared.ViewModels
{
    public class UpdateClassFresherInfoViewModel
    {
        public Guid Id { get; set; }

        public string? ClassCode { get; set; }

        public string? CLassName { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public string? RRCode { get; set; }

        public string? Location { get; set; }

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

        public bool IsDeleted { get; set; }

        public List<FresherViewModel> Freshers { get; set; }
    }
}
