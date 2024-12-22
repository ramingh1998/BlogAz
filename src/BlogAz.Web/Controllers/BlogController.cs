using BlogAz.Application.DTOs.Blogs;
using BlogAz.Facade.Blogs;
using BlogAz.Facade.Categories;
using Microsoft.AspNetCore.Mvc;

namespace BlogAz.Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogFacade _blogFacade;
        private readonly ICategoryFacade _categoryFacade;

        public BlogController(IBlogFacade blogFacade, ICategoryFacade categoryFacade)
        {
            _blogFacade = blogFacade;
            _categoryFacade = categoryFacade;
        }

        public async Task<IActionResult> Index(BlogFilterParams filterParams)
        {
            var model = await _blogFacade.GetBlogsByFilterAsync(filterParams);
            ViewBag.FilterParams = filterParams;
            ViewBag.Categories = await _categoryFacade.GetCategoriesForComboBoxAsync();
            return View(model);
        }

        public async Task<IActionResult> Details(long id)
        {
            var blog = await _blogFacade.GetBlogByIdAsync(id);
            return View(blog);
        }
    }
}
