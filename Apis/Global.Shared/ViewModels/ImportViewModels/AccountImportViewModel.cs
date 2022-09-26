using Ganss.Excel;
using System;

namespace Global.Shared.ViewModels.ImportViewModels
{
    public class AccountImportViewModel
    {
        private string _name = null!;

        [Column("Account", MappingDirections.ExcelToObject)]
        public string Name
        {
            get { return _name; }

            set
            {
                _name = value;
                EmailFsoft = $"{value.ToLower()}@fsoft.com.vn";
            }
        }

        public string HashPassword { get; set; } = null!;

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public string EmailFsoft { get; set; } = null!;

        [Column("Job Rank", MappingDirections.ExcelToObject)]
        public string Role { get; set; } = null!;

        public string Status { get; set; } = null!;
    }
}
