using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global.Shared.ViewModels.AuditManagementViewModels
{
    public class GetAuditAndAuditorViewModel
    {
        public GetAuditByClassIdAndNumberAuditViewModel GetAuditByClassIdAndNumberAuditViewModel { get; set; }
        public List<AuditorViewModel> AuditorViewModels { get; set; }
    }
}
