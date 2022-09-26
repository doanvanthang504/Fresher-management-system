
using Ganss.Excel;
using System;

namespace Global.Shared.ViewModels.ImportViewModels
{
    public class FresherImportViewModel
    {
        [Column("Account", MappingDirections.ExcelToObject)]
        public string AccountName { get; set; } = null!;

        [Column("Full Name", MappingDirections.ExcelToObject)]
        public string FullName { get; set; } = null!;

        [Column("Status", MappingDirections.ExcelToObject)]
        public string Status { get; set; } = "Onboard";

        [Column("Skill", MappingDirections.ExcelToObject)]
        public string Skill { get; set; } = null!;

        [Column("Email", MappingDirections.ExcelToObject)]
        public string Email { get; set; } = null!;

        [Column("Phone", MappingDirections.ExcelToObject)]
        public string Phone { get; set; } = null!;

        [Column("DOB", MappingDirections.ExcelToObject)]
        public DateTime? Dob { get; set; } = null!;

        [Column("Uni", MappingDirections.ExcelToObject)]
        public string University { get; set; } = null!;

        [Column("Major", MappingDirections.ExcelToObject)]
        public string Major { get; set; } = null!;

        [Column("GPA", MappingDirections.ExcelToObject)]
        public decimal GPA { get; set; }

        [Column("Graduation date", MappingDirections.ExcelToObject)]
        public int Graduation { get; set; }

        [Column("RR code/Mã lớp", MappingDirections.ExcelToObject)]
        public string RRCode { get; set; } = null!;

        [Column("ENG (50)", MappingDirections.ExcelToObject)]
        public string Eng { get; set; } = null!;

        [Column("TECH (30)", MappingDirections.ExcelToObject)]
        public string Tech { get; set; } = null!;

        [Column("Onboard date", MappingDirections.ExcelToObject)]
        public DateTime OnboardDate { get; set; } 

        [Column("Contract Type", MappingDirections.ExcelToObject)]
        public string ContractType { get; set; } = null!;

        [Column("Job Rank", MappingDirections.ExcelToObject)]
        public string JobRank { get; set; } = null!;

        [Column("Salary/Học phí", MappingDirections.ExcelToObject)]
        public decimal Salary { get; set; }

        [Column("RECer", MappingDirections.ExcelToObject)]
        public string RECer { get; set; } = null!;

        [Column("Note", MappingDirections.ExcelToObject)]
        public string Note { get; set; } = null!;

        public string ClassCode { get; set; } = "";
    }
}
