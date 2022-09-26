using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global.Shared.ViewModels.ModuleResultViewModels
{
    public class UpdateFinalAuditViewModel
    {
        public Guid FresherId { get; set; }
        public Guid ClassId { get; set; }
        public string ModuleName { get; set; }
        public double PracticeScore { get; set; } = 0;
        public double AuditScore { get; set; } = 0;

    }
}
