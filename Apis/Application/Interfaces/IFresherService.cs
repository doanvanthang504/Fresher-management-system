using Global.Shared.ViewModels;
using Global.Shared.ViewModels.FresherViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IFresherService
    {
        Task<FresherViewModel> GetFresherByIdAsync(Guid id);

        Task<bool> ChangeFresherStatusAsync(List<ChangeStatusFresherViewModel> changeStatusFresherViewModels);
    }
}
