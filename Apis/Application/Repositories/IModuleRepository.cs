using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IModuleRepository : IGenericRepository<Module>
    {
        Task<List<Module>> GetModuleByPlanId(Guid planId);
    }
}
