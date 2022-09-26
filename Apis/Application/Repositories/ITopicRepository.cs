using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface ITopicRepository : IGenericRepository<Topic>
    {
        Task<List<Topic>> GetByModuleId(Guid moduleId);
    }
}
