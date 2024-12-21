using BlogAz.Application.DTOs.Blogs;
using BlogAz.Application.DTOs.Categories;
using BlogAz.Domain.Interfaces.Blogs;
using Common.Query;

namespace BlogAz.Application.Queries.Blogs.GetByFilter
{
    public class GetBlogsByFilterQueryHandler : IQueryHandler<GetBlogsByFilterQuery, BlogFilterResult>
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IBlogCategoryRepository _blogCategoryRepository;

        public GetBlogsByFilterQueryHandler(IBlogRepository blogRepository, IBlogCategoryRepository blogCategoryRepository)
        {
            _blogRepository = blogRepository;
            _blogCategoryRepository = blogCategoryRepository;
        }

        public async Task<BlogFilterResult> Handle(GetBlogsByFilterQuery request, CancellationToken cancellationToken)
        {
            var result = _blogRepository.GetQueryable(null, q => q.CreatedAt, q => q.BlogCategories);
            var categories = _blogCategoryRepository.GetQueryable();
            if (string.IsNullOrWhiteSpace(request.FilterParams.Title) == false)
            {
                result = result.Where(q => q.Title.Contains(request.FilterParams.Title));
            }

            var skip = (request.FilterParams.PageId - 1) * request.FilterParams.Take;
            var model = new BlogFilterResult()
            {
                Data = result.Skip(skip).Take(request.FilterParams.Take).Select(q => new BlogFilterData
                {
                    CreatedAt = q.CreatedAt,
                    UpdatedAt = q.UpdatedAt.Value,
                    Id = q.Id,
                    Title = q.Title,
                    Content = q.Content,
                    ImageName = q.ImageName,
                    Categories = categories.Where(q => q.BlogId == q.Id).Select(q => new CategoryDto
                    {
                        Id = q.CategoryId,
                        Name = q.Category.Name,
                    }).ToList()
                }).ToList()
            };
            model.GeneratePaging(result, request.FilterParams.Take, request.FilterParams.PageId);
            return model;
        }
    }
}
