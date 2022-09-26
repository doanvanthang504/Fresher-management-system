using Domain.Enums;
using System;

namespace Global.Shared.ModelExport.ModelExportConfiguration
{
    public class ExportScoreViewModel
    {
        public Guid ClassId { get; set; }
        public string? ModuleName { get; set; }
        public Guid FresherId { get; set; }
        public string? FresherName { get; set; }
        public string? AccountName { get; set; }
        public TypeScoreEnum TypeScore { get; set; }
        public double ModuleScore { get; set; }
    }
}
