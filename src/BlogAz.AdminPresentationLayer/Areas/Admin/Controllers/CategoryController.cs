using BlogAz.Application.Commands.Categories.Add;
using BlogAz.Application.Commands.Categories.Delete;
using BlogAz.Application.Commands.Categories.Edit;
using BlogAz.Application.DTOs.Categories;
using BlogAz.Facade.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogAz.AdminPresentationLayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
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
        public async Task<IActionResult> AddCategory(string title, long? parentId)
        {
            var result = await _categoryFacade.AddCategoryAsync(new AddCategoryCommand(title, parentId));
            return new JsonResult(result);
        }

        public async Task<IActionResult> Edit(long id)
        {
            var category = await _categoryFacade.GetCategoryByIdAsync(id);
            return PartialView("_Edit", category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] CategoryDto model)
        {
            var result = await _categoryFacade.EditCategoryAsync(new EditCategoryCommand(model.Id, model.Name));
            return new JsonResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(long id)
        {
            var result = await _categoryFacade.DeleteCategoryAsync(new DeleteCategoryCommand(id));

            return new JsonResult(result);
        }

        public async Task<IActionResult> GetSubCategories(long parentCategoryId)
        {
            var subCategories = await _categoryFacade.GetCategoriesByParentIdAsync(parentCategoryId);
            if (!subCategories.Any())
            {
                return Json(new { success = false });
            }
            return Json(new { success = true, subCategories });
        }
    }
}
