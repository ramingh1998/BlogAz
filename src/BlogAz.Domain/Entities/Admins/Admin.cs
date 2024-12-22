using Common.Domain;

namespace BlogAz.Domain.Entities.Admins
{
    public class Admin : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Picture { get; set; }
    }
}
