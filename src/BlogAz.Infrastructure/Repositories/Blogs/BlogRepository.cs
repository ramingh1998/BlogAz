using BlogAz.Domain.Entities.Blogs;
using BlogAz.Domain.Interfaces.Blogs;
using BlogAz.Infrastructure.Persistence;
using Common.Infrastructure.Repository;

namespace BlogAz.Infrastructure.Repositories.Blogs
{
    public class BlogRepository : BaseRepository<Blog, AppDbContext>, IBlogRepository
    {
        public BlogRepository(AppDbContext context) : base(context)
        {
        }
    }
}
