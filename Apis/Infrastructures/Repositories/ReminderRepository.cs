using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;

namespace Infrastructures.Repositories
{
    public class ReminderRepository : GenericRepository<Reminder>, IReminderRepository
    {
        public ReminderRepository(
            ICurrentTime timeService,
            IClaimsService claimsService,
            AppDbContext appDbContext)
            : base(appDbContext, timeService, claimsService)
        {
        }
    }
}
