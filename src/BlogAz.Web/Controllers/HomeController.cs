using System.Diagnostics;
using BlogAz.Application.DTOs.Blogs;
using BlogAz.Facade.Blogs;
using BlogAz.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogAz.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogFacade _blogFacade;

        public HomeController(ILogger<HomeController> logger, IBlogFacade blogFacade)
        {
            _logger = logger;
            _blogFacade = blogFacade;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _blogFacade.GetBlogsByFilterAsync(new BlogFilterParams
            {
                Take = 6
            });
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
