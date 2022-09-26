using System;

namespace Domain.Entities
{
    public class SyllabusDetail : BaseEntity
    {
        public Guid ClassId { get; set; }
        public Guid PlanInformationId { get; set; }      
        public string? TopicName { get; set; }

        public string ChapterName { get; set; }

        public int DayStart { get; set; }

        public string? LectureName { get; set; }

        public string Content { get; set; }

        public string LearningObjectives { get; set; }

        public string DeliveryType { get; set; }

        //Duration(Min)
        public string Duration { get; set; }

        public string NoteDetail { get; set; }
    }
}
