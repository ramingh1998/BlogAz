using BlogAz.Domain.Entities.Admins;
using BlogAz.Domain.Interfaces.Admins;
using BlogAz.Infrastructure.Persistence;
using Common.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace BlogAz.Infrastructure.Repositories.Admins
{
    public class AdminRepository : BaseRepository<Admin, AppDbContext>, IAdminRepository
    {
        public AdminRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Admin?> GetAdminByUserNameAsync(string userName)
        {
            var result = await Context.Admins.SingleOrDefaultAsync(q => q.UserName == userName);
            if (result == null)
            {
                return null;
            }
            return result;
        }
    }
}
