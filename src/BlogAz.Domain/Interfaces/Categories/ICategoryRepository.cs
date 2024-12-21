using BlogAz.Domain.Entities.Categories;
using Common.Domain.Repository;

namespace BlogAz.Domain.Interfaces.Categories
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<List<long>> GetFinalSubCategoryIdAsync(List<long> categoryIds);
    }
}
