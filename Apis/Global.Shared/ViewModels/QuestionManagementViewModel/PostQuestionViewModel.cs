using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global.Shared.ViewModels.QuestionManagementViewModel
{
    public class PostQuestionViewModel
    {
        public string ModuleName { get; set; }
        public byte NumberAudit { get; set; }
        public List<QuestionManagementViewModel> questionManagementViewModels { get; set; }
    }
}
