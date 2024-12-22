using Common.Query;

namespace BlogAz.Application.DTOs.Admins
{
    public class AdminDto : BaseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Picture { get; set; }
    }
}
