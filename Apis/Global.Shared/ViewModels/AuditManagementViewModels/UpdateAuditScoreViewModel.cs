using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global.Shared.ViewModels.AuditManagementViewModels
{
    public class UpdateAuditScoreViewModel
    {
        public Guid Id { get; set; }
        public Evaluate? EvaluateQ1 { get; set; }
        public Evaluate? EvaluateQ2 { get; set; }
        public Evaluate? EvaluateQ3 { get; set; }
        public Evaluate? EvaluateQ4 { get; set; }
    }
}
