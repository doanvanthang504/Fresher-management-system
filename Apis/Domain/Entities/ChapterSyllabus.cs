using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class ChapterSyllabus:BaseEntity
    {
        public string? Name { get; set; }
        public Guid TopicId { get; set; }
        public Topic? Topic { get; set; }
        public double? Duration { get; set; }
        public int? Order { get; set; }
        public ICollection<LectureChapter>? LectureChapters { get; set; }
    }
}
