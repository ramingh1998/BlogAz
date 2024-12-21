using BlogAz.Application.Commands.Categories.Add;
using BlogAz.Application.Commands.Categories.Delete;
using BlogAz.Facade.Categories;
using Microsoft.AspNetCore.Mvc;

namespace BlogAz.AdminPresentationLayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryFacade _categoryFacade;

        public CategoryController(ICategoryFacade categoryFacade)
        {
            _categoryFacade = categoryFacade;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryFacade.GetAllCategoriesAsync();
            return View(categories);
        }

        [HttpPost]
        public async Task<IActionResult> AddSubCategory(string title, long parentId)
        {
            var result = await _categoryFacade.AddCategoryAsync(new AddCategoryCommand(title, parentId));
            return new JsonResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(long id)
        {
            var result = await _categoryFacade.DeleteCategoryAsync(new DeleteCategoryCommand(id));

            return new JsonResult(result);
        }
    }
}
