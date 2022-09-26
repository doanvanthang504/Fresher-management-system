using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global.Shared.ViewModels.AuditManagementViewModels
{
    public class GetAuditByClassIdAndNumberAuditViewModel
    {
         public Guid classId { get; set; }
         public string nameModule { get; set; }
         public byte numberAudit { get; set; }
    }
}
