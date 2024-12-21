using BlogAz.Application.DTOs.Categories;
using BlogAz.Domain.Entities.Categories;
using BlogAz.Domain.Interfaces.Categories;
using Common.Query;

namespace BlogAz.Application.Queries.Categories.GetById
{
    public class GetCategoryByIdQueryHandler : IQueryHandler<GetCategoryByIdQuery, CategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetTracking(request.Id);
            if (category == null)
            {
                return null;
            }
            return await MapToCategoryDtoWithSubCategories(category);
        }

        private async Task<CategoryDto> MapToCategoryDtoWithSubCategories(Category category)
        {
            var subCategories = await _categoryRepository.ToListAsync(c => c.ParentId == category.Id);

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                ParentId = category.ParentId,
                CreatedAt = category.CreatedAt,
                SubCategories = subCategories.Select(async subCategory => await MapToCategoryDtoWithSubCategories(subCategory)).Select(task => task.Result).ToList()
            };
        }
    }
}
