using System.Collections.Generic;

namespace Domain.Entities
{
    public class Plan:BaseEntity
    {
        public string? CourseName { get; set; }

        public string? CourseCode { get; set; }

        public ICollection<Module>? Modules { get; set; }
    }
}
