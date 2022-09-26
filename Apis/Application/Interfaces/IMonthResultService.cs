using Global.Shared.ViewModels.MonthResultViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IMonthResultService
    {
        Task<IList<MonthResultViewModel>> GetMonthResultByClassId(Guid classId, int month, int year);
    }
}
