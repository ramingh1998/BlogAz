using BlogAz.Domain.Entities.Admins;
using BlogAz.Domain.Entities.Users;
using Common.Domain.Repository;

namespace BlogAz.Domain.Interfaces.Admins
{
    public interface IAdminRepository : IBaseRepository<Admin>
    {
        Task<Admin?> GetAdminByUserNameAsync(string userName);
    }
}
