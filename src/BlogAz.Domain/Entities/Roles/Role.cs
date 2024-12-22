using BlogAz.Domain.Entities.Users;
using Common.Domain;

namespace BlogAz.Domain.Entities.Roles
{
    public class Role : BaseEntity
    {
        public string Title { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
