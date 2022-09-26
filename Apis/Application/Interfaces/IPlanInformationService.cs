using Global.Shared.ViewModels.PlanInfomationViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPlanInformationService
    {
        Task<List<PlanInformationViewModel>?> GetPlanDetailByClassIdAsync(Guid classId);

        Task<bool?> UpdatePlanInfoAsync(
                    Guid planId, PlanInformationViewModel planInformationViewModels);
        Task<List<WeightedNumberViewModel>> GetWeightedNumberOfModuleAsync(Guid classId);
        Task<WeightedNumberViewModel> UpdateWeightedNumberOfModuleAsync(
                                     Guid classId, WeightedNumberViewModel weightedNumberViewModel);
    }
}
