using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogAz.Application.DTOs.Categories;
using BlogAz.Domain.Entities.Categories;
using BlogAz.Domain.Interfaces.Blogs;
using BlogAz.Domain.Interfaces.Categories;
using Common.Query;

namespace BlogAz.Application.Queries.Categories.GetList
{
    public class GetAllCategoriesQueryHandler : IQueryHandler<GetAllCategoriesQuery, List<CategoryDto>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBlogCategoryRepository _blogCategoryRepository;

        public GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository, IBlogCategoryRepository blogCategoryRepository)
        {
            _categoryRepository = categoryRepository;
            _blogCategoryRepository = blogCategoryRepository;
        }

        public async Task<List<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.ToListAsync();
            var categoryDtos = BuildHierarchy(categories);
            return categoryDtos;
        }

        private List<CategoryDto> BuildHierarchy(List<Category> categories, long? parentId = null)
        {
            var blogCategories = _blogCategoryRepository.GetQueryable();
            return categories.Where(c => c.ParentId == parentId).Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                ParentId = c.ParentId,
                BlogCount = blogCategories.Where(q => q.CategoryId == c.Id).Count(),
                SubCategories = BuildHierarchy(categories, c.Id)
            }).ToList();
        }
    }
}
