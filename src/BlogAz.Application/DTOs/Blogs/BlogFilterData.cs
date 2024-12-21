using BlogAz.Application.DTOs.Categories;
using Common.Query;

namespace BlogAz.Application.DTOs.Blogs
{
    public class BlogFilterData : BaseDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
        public string Content { get; set; }
        public List<CategoryDto> Categories { get; set; }
    }
}
