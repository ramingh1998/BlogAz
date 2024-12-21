using BlogAz.Application.Commands.Categories.Add;
using BlogAz.Application.Commands.Categories.Delete;
using BlogAz.Application.Commands.Categories.Edit;
using BlogAz.Application.DTOs.Categories;
using Common.Application;

namespace BlogAz.Facade.Categories
{
    public interface ICategoryFacade
    {
        Task<OperationResult> AddCategoryAsync(AddCategoryCommand command);
        Task<OperationResult> EditCategoryAsync(EditCategoryCommand command);
        Task<OperationResult> DeleteCategoryAsync(DeleteCategoryCommand command);
        Task<List<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDto> GetCategoryByIdAsync(long id);
    }
}
