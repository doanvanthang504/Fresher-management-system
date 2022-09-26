using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;

namespace Infrastructures.Repositories
{
    public class ChemicalRepository : GenericRepository<Chemical>, IChemicalRepository
    {
        public ChemicalRepository(AppDbContext chemicalDbContext,
            ICurrentTime timeService,
            IClaimsService claimsService)
            : base(chemicalDbContext,
                  timeService,
                  claimsService) { }
    }
}
