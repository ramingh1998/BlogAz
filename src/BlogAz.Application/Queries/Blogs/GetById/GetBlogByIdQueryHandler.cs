using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogAz.Application.DTOs.Blogs;
using BlogAz.Application.DTOs.Categories;
using BlogAz.Domain.Interfaces.Blogs;
using Common.Application;
using Common.Query;

namespace BlogAz.Application.Queries.Blogs.GetById
{
    public class GetBlogByIdQueryHandler : IQueryHandler<GetBlogByIdQuery, BlogDto>
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IBlogCategoryRepository _blogCategoryRepository;

        public GetBlogByIdQueryHandler(IBlogRepository blogRepository, IBlogCategoryRepository blogCategoryRepository)
        {
            _blogRepository = blogRepository;
            _blogCategoryRepository = blogCategoryRepository;
        }

        public async Task<BlogDto> Handle(GetBlogByIdQuery request, CancellationToken cancellationToken)
        {
            var blog = await _blogRepository.GetTracking(request.Id);
            if (blog == null)
            {
                return null;
            }
            var categories = _blogCategoryRepository.GetQueryable();
            var blogDto = new BlogDto
            {
                Id = blog.Id,
                Title = blog.Title,
                Content = blog.Content,
                ImageName = blog.ImageName,
                CreatedAt = blog.CreatedAt,
                UpdatedAt = blog.UpdatedAt.Value,
                Categories = categories.Where(q => q.BlogId == blog.Id).Select(q => new CategoryDto
                {
                    CreatedAt = q.CreatedAt,
                    Id = q.CategoryId,
                    Name = q.Category.Name,
                    ParentId = q.Category.ParentId,
                    UpdatedAt = q.UpdatedAt.Value,
                }).ToList(),
            };
            return blogDto;
        }
    }
}
