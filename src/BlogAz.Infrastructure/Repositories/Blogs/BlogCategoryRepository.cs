using BlogAz.Domain.Entities.Blogs;
using BlogAz.Domain.Interfaces.Blogs;
using BlogAz.Infrastructure.Persistence;
using Common.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace BlogAz.Infrastructure.Repositories.Blogs
{
    public class BlogCategoryRepository : BaseRepository<BlogCategory, AppDbContext>, IBlogCategoryRepository
    {
        public BlogCategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<BlogCategory>> GetBlogCategoriesByBlogIdAsync(long blogId)
        {
            var blogCategories = await Context.BlogCategories.Where(bc => bc.BlogId == blogId).ToListAsync();
            return blogCategories;
        }
    }
}
