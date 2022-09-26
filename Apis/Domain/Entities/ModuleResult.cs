using System;

namespace Domain.Entities
{
    public class ModuleResult : BaseEntity
    {
        public Guid FresherId { get; set; }

        public Guid ClassId { get; set; }

        public Fresher Fresher { get; set; }

        public string ModuleName { get; set; }

        private double? _assignmentAvgScore;

        public double? AssignmentAvgScore
        {
            get { return _assignmentAvgScore; }
            set { _assignmentAvgScore = Math.Round(value.Value, 1); }
        }

        private double? _quizzAvgScore;
        public double? QuizzAvgScore
        {
            get { return _quizzAvgScore; }
            set { _quizzAvgScore = Math.Round(value.Value, 1); }
        }
        private double? _finalAuditScore;
        public double? FinalAuditScore
        {
            get { return _finalAuditScore; }
            set { _finalAuditScore = Math.Round(value.Value, 1); }
        }
        private double? _finalMark;
        public double? FinalMark
        {
            get { return _finalMark; }
            set { _finalMark = Math.Round(value.Value, 1); }
        }
    }
}
