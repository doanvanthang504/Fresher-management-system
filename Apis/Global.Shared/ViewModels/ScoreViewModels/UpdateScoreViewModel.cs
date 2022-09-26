using Domain.Enums;
using System;

namespace Global.Shared.ViewModels.ScoreViewModels
{
    public class UpdateScoreViewModel
    {
        public Guid Id { get; set; }

        public TypeScoreEnum TypeScore { get; set; }

        public double ModuleScore { get; set; }
    }
}
