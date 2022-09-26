using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;

namespace Infrastructures.Repositories
{
    public class PlanRepository : GenericRepository<Plan>, IPlanRepository
    {

        public PlanRepository(AppDbContext dbContext,
                              ICurrentTime currentTime,
                              IClaimsService claimsService)
            : base(dbContext, currentTime, claimsService)
        {
        }
    }
}
