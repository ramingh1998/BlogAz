using BlogAz.Application.DTOs.Categories;
using BlogAz.Domain.Interfaces.Categories;
using Common.Query;

namespace BlogAz.Application.Queries.Categories.GetForComboBox
{
    public class GetAllCategoriesForComboBoxQueryHandler : IQueryHandler<GetAllCategoriesForComboBoxQuery, List<CategoryDto>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetAllCategoriesForComboBoxQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryDto>> Handle(GetAllCategoriesForComboBoxQuery request, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.ToListAsync();

            return categories.Select(q => new CategoryDto
            {
                Id = q.Id,
                Name = q.Name,
            }).ToList();
        }
    }
}
