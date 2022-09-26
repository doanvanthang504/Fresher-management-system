using System;

namespace Global.Shared.ViewModels.LectureChapterViewModels
{
    public class LectureChapterAddViewModel
    {
        public string? Lecture { get; set; }

        public Guid ChapterSyllabusId { get; set; }

        public string Content { get; set; }

        public string? LearningObjectives { get; set; }

        public string DeliveryType { get; set; }

        //Duration(min)
        public double Duration { get; set; }

        public int Order { get; set; }

        public string? NoteDetail { get; set; }
    }
}
