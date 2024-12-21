using BlogAz.Domain.Entities.Users;
using BlogAz.Domain.Interfaces.Users;
using BlogAz.Infrastructure.Persistence;
using Common.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace BlogAz.Infrastructure.Repositories.Users
{
    public class UserRepository : BaseRepository<User, AppDbContext>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var user = await Context.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null)
            {
                return null;
            }
            return user;
        }
    }
}
