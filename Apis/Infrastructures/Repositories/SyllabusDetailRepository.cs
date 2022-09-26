using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;

namespace Infrastructures.Repositories
{
    public class SyllabusDetailRepository :
                        GenericRepository<SyllabusDetail>,
                        ISyllabusDetailRepository
    {
        public SyllabusDetailRepository(
                            AppDbContext dbContext,
                            ICurrentTime currentTime,
                            IClaimsService claimsService)
            : base(dbContext, currentTime, claimsService)
        {
        }
    }
}
