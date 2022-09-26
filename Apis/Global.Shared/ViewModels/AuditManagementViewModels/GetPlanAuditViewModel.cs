using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global.Shared.ViewModels.AuditManagementViewModels
{
    public class GetPlanAuditViewModel
    {
        public Guid Id { get; set; }
        public Guid ClassFresherId { get; set; }
        public string ModuleName { get; set; }
        public Byte NumberAudit { get; set; }
        public DateTime? DateStart { get; set; }

    }
}
