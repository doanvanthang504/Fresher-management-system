

using System;

namespace Global.Shared.ViewModels.ClassFresherViewModels
{
    public class UpdateClassFresherViewModel
    {
        public Guid Id { get; set; }
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
        public Guid PlanId { get; set; }
    }
}
