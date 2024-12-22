using BlogAz.AdminPresentationLayer.Areas.Admin.ViewModels.Blogs;
using BlogAz.Application.Commands.Blogs.Add;
using BlogAz.Application.Commands.Blogs.Delete;
using BlogAz.Application.Commands.Blogs.Edit;
using BlogAz.Application.DTOs.Blogs;
using BlogAz.Application.DTOs.Categories;
using BlogAz.Domain.Entities.Categories;
using BlogAz.Facade.Blogs;
using BlogAz.Facade.Categories;
using Common.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogAz.AdminPresentationLayer.Areas.Admin.Controllers
{
    [Area("Admin")]
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
            var result = await _blogFacade.GetBlogsByFilterAsync(filterParams);
            var categories = await _categoryFacade.GetMainCategoriesAsync();
            ViewBag.CategoryList = new SelectList(categories, "Id", "Name", filterParams.CategoryId);
            if (filterParams.CategoryId.HasValue)
            {
                var subCategories = await _categoryFacade.GetCategoriesByParentIdAsync(filterParams.CategoryId.Value);
                ViewBag.SubCategoryList = new SelectList(subCategories, "Id", "Name");
            }
            ViewBag.FilterParams = filterParams;
            ViewBag.MainCategories = await _categoryFacade.GetMainCategoriesAsync();
            return View(result);
        }

        public async Task<IActionResult> Add()
        {
            var categories = await _categoryFacade.GetAllCategoriesAsync();
            var categoryTree = BuildCategoryTree(categories);
            ViewBag.CategoryTreeJson = Newtonsoft.Json.JsonConvert.SerializeObject(categoryTree);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(BlogViewModel model)
        {

            var result = await _blogFacade.AddBlogAsync(new AddBlogCommand
            {
                Title = model.Title,
                Content = model.Content,
                ImageFile = model.ImageFile,
                CategoryIds = model.CategoryIds,
            });
            return new JsonResult(result);
        }

        public async Task<IActionResult> Edit(long id)
        {
            var blog = await _blogFacade.GetBlogByIdAsync(id);
            var model = new BlogViewModel
            {
                Id = blog.Id,
                Title = blog.Title,
                Content = blog.Content,
                ImageName = blog.ImageName,
                CategoryIds = blog.Categories.Select(c => c.Id).ToList()
            };
            var categories = await _categoryFacade.GetCategoriesForComboBoxAsync();
            ViewBag.CategoryList = categories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToList();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BlogViewModel model)
        {
            var result = await _blogFacade.EditBlogAsync(new EditBlogCommand
            {
                Id = model.Id.Value,
                Title = model.Title,
                Content = model.Content,
                ImageFile = model.ImageFile,
                CategoryIds = model.CategoryIds,
            });
            return new JsonResult(result);
        }

        public async Task<IActionResult> Details(long id)
        {
            var blog = await _blogFacade.GetBlogByIdAsync(id);
            return View(blog);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _blogFacade.DeleteBlogAsync(new DeleteBlogCommand(id));
            return new JsonResult(result);
        }

        private List<CategoryDto> BuildCategoryTree(List<CategoryDto> categories)
        {
            var categoryTree = categories.Where(c => c.ParentId == null).Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                SubCategories = c.SubCategories
            }).ToList();

            return categoryTree;
        }
    }
}
