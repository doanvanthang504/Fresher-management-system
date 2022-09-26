using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IPlanInformationRepository : IGenericRepository<PlanInformation>
    {
        Task<List<PlanInformation>> GetByClassIdAsync(Guid classId);

        Task<PlanInformation> GetByModuleNameAndClassIdAsync(Guid classId, string moduleName);
    }
}
