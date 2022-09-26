using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global.Shared.ViewModels.ModuleResultViewModels
{
    public class UpdateQuizzAssignAVGViewModel
    {
        public Guid FresherId { get; set; }
        public Guid ClassId { get; set; }
        public string ModuleName { get; set; }
        public TypeScoreEnum TypeScore { get; set; }
        public IList<double> Scores { get; set; }
    }
}
