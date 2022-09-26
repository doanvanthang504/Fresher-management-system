using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infrastructures.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context,
                                    ICurrentTime timeService,
                                    IClaimsService claimsService)
                                    : base(context,
                                          timeService,
                                          claimsService)
        { }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            username = username.Trim();

            return await _dbSet.FirstOrDefaultAsync(x => x.Username!.Equals(username));
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            email = email.Trim().ToLower();

            return await _dbSet.FirstOrDefaultAsync(x => x.Email!.Equals(email));
        }

        // Adds user from excel file.
        // public async Task AddAsync(User user)
        // {

        // }
    }
}
