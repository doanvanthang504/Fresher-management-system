using System;

namespace Domain.Entities
{
    public class MonthResult : BaseEntity
    {
        public Guid FresherId { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public double GPA { get; set; }

        public double Disciplinary { get; set; }

        public double AcademicMark { get; set; }
    }
}
