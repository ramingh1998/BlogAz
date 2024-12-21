using BlogAz.Domain.Entities.Blogs;
using Common.Domain.Repository;

namespace BlogAz.Domain.Interfaces.Blogs
{
    public interface IBlogCategoryRepository : IBaseRepository<BlogCategory>
    {
        Task<List<BlogCategory>> GetBlogCategoriesByBlogIdAsync(long blogId);
    }
}
