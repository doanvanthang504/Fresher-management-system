using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Topic : BaseEntity
    {
        public string Name { get; set; }

        public Guid ModuleId { get; set; }

        public Module Module { get; set; }

        public int Order { get; set; }

        public string Pic { get; set; }

        public double Duration { get; set; }

        public string? NoteDetail { get; set; }

        public ICollection<ChapterSyllabus>? ChapterSyllabuses { get; set; }
    }
}
