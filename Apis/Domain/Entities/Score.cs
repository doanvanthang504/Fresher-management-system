using Domain.Enums;
using System;

namespace Domain.Entities
{
    public class Score : BaseEntity
    {
        public Guid FresherId { get; set; }

        public Guid ClassId { get; set; }

        public string ModuleName { get; set; }

        public TypeScoreEnum TypeScore { get; set; }

        public double ModuleScore { get; set; }
    }
}
