using Global.Shared.ViewModels.FresherViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global.Shared.ViewModels.AuditManagementViewModels
{
    public class CreateAuditViewModel
    {
        public Guid ClassFresherId { get; set; }
        public string ModuleName { get; set; }
        public string DateStart { get; set; }
        public List<AddFresherViewModel> fresherViewModels { get; set; }
    }
}
