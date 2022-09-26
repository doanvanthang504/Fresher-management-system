using Global.Shared.Commons;
using Global.Shared.ViewModels.ModuleViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IModuleService
    {
        Task<Pagination<ModuleViewModel>> GetAllModuleAsync(int pageIndex, int pageSize);
        Task<ModuleViewModel> GetModuleByIdAsync(Guid moduleId);
        Task<List<ModuleViewModel>> GetModuleByPlanIdAsync(Guid planId);
        Task<ModuleViewModel> AddModuleAsync(ModuleAddViewModel moduleAddViewModel);
        Task<ModuleViewModel> UpdateModuleAsync(Guid id, ModuleUpdateViewModel moduleUpdateViewModel);
    }
}
