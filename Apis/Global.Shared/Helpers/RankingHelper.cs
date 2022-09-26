using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global.Shared.Helpers
{
    public static class RankingHelper
    {
        public static ReportCompletionLevelEnum GetReportCompletionLevelEnum(string finalMark)
        {
            var gpa = double.Parse(finalMark.Split("%")[0]);
            if (gpa < 60)
                return ReportCompletionLevelEnum.D;
            else if (gpa < 72)
                return ReportCompletionLevelEnum.C;
            else if (gpa < 86)
                return ReportCompletionLevelEnum.B;
            else if (gpa < 93)
                return ReportCompletionLevelEnum.A;
            else
                return ReportCompletionLevelEnum.APlus;
        }
    }
}
