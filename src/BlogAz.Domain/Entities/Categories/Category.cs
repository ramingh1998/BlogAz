using BlogAz.Domain.Entities.Blogs;
using Common.Domain;

namespace BlogAz.Domain.Entities.Categories
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public long? ParentId { get; set; }
        public virtual ICollection<BlogCategory> BlogCategories { get; set; }
    }
}
