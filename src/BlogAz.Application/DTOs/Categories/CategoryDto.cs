using Common.Query;

namespace BlogAz.Application.DTOs.Categories
{
    public class CategoryDto : BaseDto
    {
        public string Name { get; set; }
        public long? ParentId { get; set; }
        public int BlogCount{ get; set; }
        public List<CategoryDto> SubCategories { get; set; } = new List<CategoryDto>();
    }
}
