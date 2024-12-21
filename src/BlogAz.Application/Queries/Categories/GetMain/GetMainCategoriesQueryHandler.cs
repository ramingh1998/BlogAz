using BlogAz.Application.DTOs.Categories;
using BlogAz.Domain.Interfaces.Categories;
using Common.Query;

namespace BlogAz.Application.Queries.Categories.GetMain
{
    public class GetMainCategoriesQueryHandler : IQueryHandler<GetMainCategoriesQuery, List<CategoryDto>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetMainCategoriesQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryDto>> Handle(GetMainCategoriesQuery request, CancellationToken cancellationToken)
        {
            var result = await _categoryRepository.ToListAsync(q => q.ParentId == null);
            var categoryDtos = result.Select(q => new CategoryDto
            {
                Id = q.Id,
                Name = q.Name
            }).ToList();
            return categoryDtos;
        }
    }
}
