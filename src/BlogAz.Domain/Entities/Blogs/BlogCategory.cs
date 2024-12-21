using BlogAz.Domain.Entities.Categories;
using Common.Domain;

namespace BlogAz.Domain.Entities.Blogs
{
    public class BlogCategory : BaseEntity
    {
        public long BlogId { get; set; }
        public long CategoryId { get; set; }
        public Blog Blog { get; set; }
        public Category Category { get; set; }
    }
}
