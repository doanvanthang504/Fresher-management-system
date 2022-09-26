using Domain.Enums;
using System;

namespace Domain.Entities
{
    public class AuditResult : BaseEntity
    {
        public Guid ClassFresherId { get; set; }

        public string? ModuleName { get; set; }

        public byte NumberAudit { get; set; }

        public Guid FresherId { get; set; }

        public string? AuditorId { get; set; }

        public string QuestionQ1 { get; set; }

        public string CommentQ1 { get; set; }

        public Evaluate? EvaluateQ1 { get; set; }

        public string QuestionQ2 { get; set; }

        public string CommentQ2 { get; set; }

        public Evaluate? EvaluateQ2 { get; set; }

        public string QuestionQ3 { get; set; }

        public string CommentQ3 { get; set; }

        public Evaluate? EvaluateQ3 { get; set; }

        public string QuestionQ4 { get; set; }

        public string CommentQ4 { get; set; }

        public Evaluate? EvaluateQ4 { get; set; }

        public DateTime DateStart { get; set; }

        public string PracticeComment { get; set; }

        public double PracticeScore { get; set; }

        public double AuditScore { get; set; }

        public Rank? Rank { get; set; }

        public string AuditComment { get; set; }

        public bool Status { get; set; }

        public ClassFresher ClassFresher { get; set; }
    }
}
