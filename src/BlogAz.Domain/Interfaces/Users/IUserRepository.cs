using BlogAz.Domain.Entities.Users;
using Common.Domain.Repository;

namespace BlogAz.Domain.Interfaces.Users
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<User?> GetUserByUserNameAsync(string userName);
    }
}
