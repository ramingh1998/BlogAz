using BlogAz.Application.DTOs.Categories;
using Common.Query;

namespace BlogAz.Application.Queries.Categories.GetById
{
    public record GetCategoryByIdQuery(long Id) : IQuery<CategoryDto>;
}
