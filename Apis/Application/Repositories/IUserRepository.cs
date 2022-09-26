using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        /// <summary>
        /// Gets a user from the database by their username.
        /// </summary>
        /// <param name="username">
        /// Username (or name of the account) of the user, case-sensitive, no whitespaces.
        /// </param>
        /// <returns>
        /// An <see cref="User"/> exists, or <see langword="null"/> if the user doesn't
        /// exist or in deleted state.
        /// </returns>
        Task<User?> GetByUsernameAsync(string username);

        /// <summary>
        /// Gets a user from the database by their main email.
        /// </summary>
        /// <param name="email">
        /// Main email of the user, not case-sensitive, no whitespaces.
        /// </param>
        /// <returns>
        /// An <see cref="User"/> exists, or <see langword="null"/> if the user doesn't
        /// exist or in deleted state.
        /// </returns>
        Task<User?> GetByEmailAsync(string email);
    }
}