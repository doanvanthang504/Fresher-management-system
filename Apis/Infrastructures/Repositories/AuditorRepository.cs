using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repositories
{
    public class AuditorRepository : GenericRepository<Auditor>, IAuditorRepository
    {
        public AuditorRepository(AppDbContext chemicalDbContext,
            ICurrentTime timeService,
            IClaimsService claimsService)
            : base(chemicalDbContext,
                  timeService,
                  claimsService)
        {
        }
    }
}
