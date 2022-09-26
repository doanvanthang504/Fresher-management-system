using Global.Shared.ViewModels.SyllabusDetailViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISyllabusDetailService
    {
        Task<IEnumerable<SyllabusDetailViewModel>> GetSyllabusDetailByClassIdAsync(Guid classId);
        Task<IEnumerable<SyllabusDetailViewModel>> GetSyllabusDetailByPlanInformationIdAsync(Guid planInformationId);
        Task<SyllabusDetailViewModel> UpdateSyllabusDetailAsync(
                                        Guid id, SyllabusDetailAddViewModel syllabusDetailAddViewModel);
    }
}
