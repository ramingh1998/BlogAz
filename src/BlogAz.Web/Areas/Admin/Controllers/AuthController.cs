using Common.Application.SecurityUtil;
using Microsoft.AspNetCore.Mvc;
using BlogAz.Web.Infrastructure.JwtUtil;
using BlogAz.Web.Areas.Admin.ViewModels.Auth;
using BlogAz.Domain.Interfaces.Users;

namespace BlogAz.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthController(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var user = await _userRepository.GetUserByUserNameAsync(model.UserName);
            if (user == null)
            {
                ModelState.AddModelError("UserName", "İstifadəçi adı və ya şifrə yanlışdır.");
                return View(model);
            }
            var verifyPassword = Sha256Hasher.IsCompare(user.Password, model.Password);
            if (!verifyPassword)
            {
                ModelState.AddModelError("UserName", "İstifadəçi tapılmadı.");
                return View(model);
            }
            var token = JwtTokenBuilder.BuildToken(user, _configuration);
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
