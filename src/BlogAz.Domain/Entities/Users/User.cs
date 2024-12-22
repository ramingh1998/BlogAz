using BlogAz.Domain.Entities.Roles;
using Common.Domain;

namespace BlogAz.Domain.Entities.Users
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Picture { get; set; }
        public long RoleId { get; set; }
        public Role Role { get; set; }
    }
}
