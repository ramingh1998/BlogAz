using BlogAz.Application.Utilities;
using BlogAz.Domain.Entities.Blogs;
using BlogAz.Domain.Interfaces.Blogs;
using BlogAz.Domain.Interfaces.Categories;
using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Common.Application.SecurityUtil;

namespace BlogAz.Application.Commands.Blogs.Add
{
    public class AddBlogCommandHandler : IBaseCommandHandler<AddBlogCommand>
    {
        private readonly IBlogRepository _blogRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBlogCategoryRepository _blogCategoryRepository;
        private readonly ILocalFileService _localFileService;

        public AddBlogCommandHandler(IBlogRepository blogRepository, ICategoryRepository categoryRepository, IBlogCategoryRepository blogCategoryRepository, ILocalFileService localFileService)
        {
            _blogRepository = blogRepository;
            _categoryRepository = categoryRepository;
            _blogCategoryRepository = blogCategoryRepository;
            _localFileService = localFileService;
        }

        public async Task<OperationResult> Handle(AddBlogCommand request, CancellationToken cancellationToken)
        {
            var blogIsExist = _blogRepository.Exists(q => q.Title == request.Title);
            if (blogIsExist)
            {
                return OperationResult.Error("Bu adla başqa bir bloq mövcuddur.");
            }

            var imageName = await _localFileService.SaveFileAndGenerateName(request.ImageFile, BlogDirectories.BlogImage);
            var content = request.Content.SanitizeText();
            var blog = new Blog
            {
                Title = request.Title,
                ImageName = imageName,
                Content = content,
                CreatedAt = DateTime.Now
            };
            _blogRepository.Add(blog);
            await _blogRepository.Save();

            var blogCategories = new List<BlogCategory>();
            foreach (var item in request.CategoryIds)
            {
                var blogCategory = new BlogCategory
                {
                    BlogId = blog.Id,
                    CategoryId = item
                };
                blogCategories.Add(blogCategory);
            }
            _blogCategoryRepository.AddRange(blogCategories);
            await _blogCategoryRepository.Save();
            return OperationResult.Success();
        }
    }
}
