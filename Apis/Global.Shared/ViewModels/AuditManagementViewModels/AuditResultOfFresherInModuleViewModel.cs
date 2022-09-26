using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global.Shared.ViewModels.AuditManagementViewModels
{
    public class AuditResultOfFresherInModuleViewModel
    {
        public Guid ClassFresherId { get; set; }
        public string ModuleName { get; set; }
        public Guid FresherId { get; set; }
    }
}
