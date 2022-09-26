using Ganss.Excel;
using System;

namespace Global.Shared.ViewModels.ImportViewModels
{
    public class ClassImportViewModel
    {

        [Column("RR code/Mã lớp", MappingDirections.ExcelToObject)]
        public string? RRCode { get; set; }

        public string? ClassCode { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string? Location { get; set; }

        public string? NameAdmin1 { get; set; }

        public string? NameAdmin2 { get; set; }

        public string? NameAdmin3 { get; set; }

        public string? NameTrainer1 { get; set; }

        public string? NameTrainer2 { get; set; }

        public string? NameTrainer3 { get; set; }

        public string? EmailAdmin1 { get; set; }

        public string? EmailAdmin2 { get; set; }

        public string? EmailAdmin3 { get; set; }

        public string? EmailTrainer1 { get; set; }

        public string? EmailTrainer2 { get; set; }

        public string? EmailTrainer3 { get; set; }

        public string Status { get; set; } = null!;
        public Guid PlanId { get; set; }

        public double Budget { get; set; }
    }
}
