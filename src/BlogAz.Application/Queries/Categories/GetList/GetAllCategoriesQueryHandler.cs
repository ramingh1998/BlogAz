using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogAz.Application.DTOs.Categories;
using BlogAz.Domain.Entities.Categories;
using BlogAz.Domain.Interfaces.Categories;
using Common.Query;

namespace BlogAz.Application.Queries.Categories.GetList
{
    public class GetAllCategoriesQueryHandler : IQueryHandler<GetAllCategoriesQuery, List<CategoryDto>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.ToListAsync();
            var categoryDtos = BuildHierarchy(categories);
            return categoryDtos;
        }

        private List<CategoryDto> BuildHierarchy(List<Category> categories, long? parentId = null)
        {
            return categories.Where(c => c.ParentId == parentId).Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                ParentId = c.ParentId,
                SubCategories = BuildHierarchy(categories, c.Id)
            }).ToList();
        }
    }
}
