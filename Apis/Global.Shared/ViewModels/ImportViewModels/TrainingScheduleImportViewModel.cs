namespace Global.Shared.ViewModels.ImportViewModels
{
    public class TrainingScheduleImportViewModel
    {
        public string TrainingUnitName { get; set; } = null!;

        public string TrainingChapterName { get; set; } = null!;

        public string Day { get; set; } = null!;

        public string Lecture { get; set; } = null!;

        public string Content { get; set; } = null!;

        public string LearningObjectives { get; set; } = null!;

        public string DeliveryType { get; set; } = null!;

        public double Duration { get; set; }

        public string GeneralNotes { get; set; } = null!;
    }
}
