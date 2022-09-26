namespace Global.Shared.ViewModels.ImportViewModels
{
    public class ScoreImportViewModel
    {
        public string ClassId { get; set; } = null!;
        public string ModuleName { get; set; } = null!;
        public string FresherId { get; set; } = null!;
        public string FresherName { get; set; } = null!;
        public string AccountName { get; set; } = null!;
        public string TypeScore { get; set; } = null!;
        public double ModuleScore { get; set; }
    }
}
