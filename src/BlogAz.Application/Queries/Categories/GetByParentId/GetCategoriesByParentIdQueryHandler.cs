using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogAz.Application.DTOs.Categories;
using BlogAz.Domain.Interfaces.Categories;
using Common.Application.SecurityUtil;
using Common.Query;

namespace BlogAz.Application.Queries.Categories.GetByParentId
{
    public class GetCategoriesByParentIdQueryHandler : IQueryHandler<GetCategoriesByParentIdQuery, List<CategoryDto>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoriesByParentIdQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryDto>> Handle(GetCategoriesByParentIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _categoryRepository.ToListAsync(q => q.ParentId == request.Id);
            if (result == null)
            {
                return null;
            }
            var categoryDtos = result.Select(q => new CategoryDto
            {
                Id = q.Id,
                Name = q.Name.Replace("\n", "").Trim()
            }).ToList();
            return categoryDtos;
        }
    }
}
