using System;

namespace Domain.Entities
{
    public class LectureChapter : BaseEntity 
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
        public ChapterSyllabus ChapterSyllabus { get; set; }

    }
}
