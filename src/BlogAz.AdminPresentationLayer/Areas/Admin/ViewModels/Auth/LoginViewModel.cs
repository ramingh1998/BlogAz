using System.ComponentModel.DataAnnotations;

namespace BlogAz.AdminPresentationLayer.Areas.Admin.ViewModels.Auth
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "İstifadəçi adı mütləqdir.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Şifrə mütləqdir.")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
