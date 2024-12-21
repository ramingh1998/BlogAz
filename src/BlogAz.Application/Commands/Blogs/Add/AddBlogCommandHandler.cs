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

            var imageName = await _localFileService.SaveFileAndGenerateName(request.ImageName, BlogDirectories.BlogImage);
            var content = request.Content.SanitizeText();
            var blog = new Blog
            {
                Title = request.Title,
                ImageName = imageName,
                Content = content,
                CreatedAt = DateTime.Now
            };
            _blogRepository.Add(blog);
            if (request.CategoryIds != null && request.CategoryIds.Any())
            {
                var finalCategoryIds = await _categoryRepository.GetFinalSubCategoryIdAsync(request.CategoryIds);
                foreach (var categoryId in finalCategoryIds)
                {
                    var blogCategory = new BlogCategory
                    {
                        Blog = blog,
                        CategoryId = categoryId
                    };
                    _blogCategoryRepository.Add(blogCategory);
                }
            }
            await _blogRepository.Save();
            return OperationResult.Success();
        }
    }
}
