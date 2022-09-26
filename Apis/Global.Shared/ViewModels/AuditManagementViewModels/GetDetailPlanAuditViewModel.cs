using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global.Shared.ViewModels.AuditManagementViewModels
{
    public class GetDetailPlanAuditViewModel
    {
        public Guid Id { get; set; }
        public string AccountName { get; set; }
        public Guid ClassFresherId { get; set; }
        public string ModuleName { get; set; }
        public Byte NumberAudit { get; set; }
        public Guid FresherId { get; set; }
        public string AuditorId { get; set; }
        public string AuditorName { get; set; }
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
        public string PracticeComment { get; set; }
        public double PracticeScore { get; set; }
        public double AuditScore { get; set; }
        public string Rank { get; set; }
        public string AuditComment { get; set; }
        public bool Status { get; set; }

    }
}
