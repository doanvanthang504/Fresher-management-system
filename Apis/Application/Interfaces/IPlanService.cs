using Global.Shared.Commons;
using Global.Shared.ViewModels.PlanInfomationViewModels;
using Global.Shared.ViewModels.PlanViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPlanService
    {
        Task<Pagination<PlanGetViewModel>> GetAllPlanAsync(int pageIndex, int pageSize);
        Task<PlanGetViewModel> AddPlanAsync(PlanAddViewModel planAddViewModel);
        Task<PlanGetViewModel> GetPlanByIdAsync(Guid planId);
        Task<PlanGetViewModel> UpdatePlanAsync(Guid planId, PlanUpdateViewModel planUpdateViewModel);
        Task<List<PlanInformationViewModel>> ChoosePlanForClassAsync(ChoosePlanForClassViewModel planInfomation);
        //Task SeedDataPlans();
    }
}
