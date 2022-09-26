using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructures.Repositories
{
    public class TopicRepository : GenericRepository<Topic>, ITopicRepository
    {
        public TopicRepository(AppDbContext dbContext,
                              ICurrentTime currentTime,
                              IClaimsService claimsService)
            : base(dbContext, currentTime, claimsService)
        {
        }

        public async Task<List<Topic>> GetByModuleId(Guid moduleId)
        {
            return await _dbSet.Where(x => x.ModuleId == moduleId)
                               .OrderBy(x=>x.Order)
                               .ToListAsync();
        }
    }
}
