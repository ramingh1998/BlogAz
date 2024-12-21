using BlogAz.Domain.Entities.Categories;
using BlogAz.Domain.Interfaces.Categories;
using BlogAz.Infrastructure.Persistence;
using Common.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace BlogAz.Infrastructure.Repositories.Categories
{
    public class CategoryRepository : BaseRepository<Category, AppDbContext>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<long>> GetFinalSubCategoryIdAsync(List<long> categoryIds)
        {
            var categories = Context.Categories.AsQueryable();

            var finalCategoryIds = categoryIds
                .Where(categoryId => !categories.Any(c => c.ParentId == categoryId && categoryIds.Contains(c.Id)))
                .ToList();

            return finalCategoryIds;
        }
    }
}
