using BlogAz.Application.DTOs.Categories;
using Common.Query;

namespace BlogAz.Application.Queries.Categories.GetByParentId
{
    public record GetCategoriesByParentIdQuery(long Id) : IQuery<List<CategoryDto>>;
}
