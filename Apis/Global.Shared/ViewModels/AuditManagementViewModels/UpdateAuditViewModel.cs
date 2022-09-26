using Domain.Enums;
using System;

namespace Global.Shared.ViewModels.AuditManagementViewModels
{
    public class UpdateAuditViewModel
    {
        public string AuditorId { get; set; }
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
        public DateTimeOffset StartDate { get; set; }

        public string PracticeComment { get; set; }
        public decimal PracticeScore { get; set; }
        public string AuditComment { get; set; }
        public bool Status { get; set; }

    }
}
