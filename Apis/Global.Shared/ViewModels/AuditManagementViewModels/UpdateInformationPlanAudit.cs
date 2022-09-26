using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global.Shared.ViewModels.AuditManagementViewModels
{
    public class UpdateInformationPlanAudit
    {
        public Guid classId { get; set; }
        public string nameModuleOld { get; set; }
        public string nameModuleNew { get; set; }
        public byte numberAudit { get; set; }
        public string dateStart { get; set; }
    }
}
