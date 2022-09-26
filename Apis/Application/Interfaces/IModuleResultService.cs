using Domain.Entities;
using Global.Shared.ViewModels.ModuleResultViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IModuleResultService
    {
        Task<bool> CreateModuleResultAsync (CreateModuleResultViewModel createModuleResultViewModel);
        Task UpdateModuleResultAsync(UpdateQuizzAssignAVGViewModel updateModuleResultViewModel);
        Task<bool> UpdateModuleResultAsync(UpdateFinalAuditViewModel updateFinalAuditViewModel);
        
        Task<IList<ModuleResultViewModel>> 
            GetModuleResultByClassIdAndModuleNameAsync(
                                                     Guid classId, string moduleName);
    }
}
