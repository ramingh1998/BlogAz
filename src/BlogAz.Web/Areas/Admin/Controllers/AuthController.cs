using BlogAz.Domain.Interfaces.Admins;
using Common.Application.SecurityUtil;
using Microsoft.AspNetCore.Mvc;
using BlogAz.Web.Infrastructure.JwtUtil;
using BlogAz.Web.Areas.Admin.ViewModels.Auth;

namespace BlogAz.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthController : Controller
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IConfiguration _configuration;

        public AuthController(IAdminRepository adminRepository, IConfiguration configuration)
        {
            _adminRepository = adminRepository;
            _configuration = configuration;
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var admin = await _adminRepository.GetAdminByUserNameAsync(model.UserName);
            if (admin == null)
            {
                ModelState.AddModelError("UserName", "İstifadəçi adı və ya şifrə yanlışdır.");
                return View(model);
            }
            var verifyPassword = Sha256Hasher.IsCompare(admin.Password, model.Password);
            if (!verifyPassword)
            {
                ModelState.AddModelError("UserName", "İstifadəçi tapılmadı.");
                return View(model);
            }
            var token = JwtTokenBuilder.BuildToken(admin, _configuration);
            HttpContext.Response.Cookies.Append("Token", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = model.RememberMe ? DateTimeOffset.MaxValue : DateTimeOffset.UtcNow.AddDays(7),
            });
            return RedirectToAction("Index", "Home");
        }
    }
}
