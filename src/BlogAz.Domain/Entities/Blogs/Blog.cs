using Common.Domain;

namespace BlogAz.Domain.Entities.Blogs
{
    public class Blog : BaseEntity
    {
        public string Title { get; set; }
        public string ImageName { get; set; }
        public string Content { get; set; }
        public virtual ICollection<BlogCategory> BlogCategories { get; set; }
    }
}
