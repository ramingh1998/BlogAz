using BlogAz.Application.Utilities;
using BlogAz.Domain.Entities.Blogs;
using BlogAz.Domain.Interfaces.Blogs;
using BlogAz.Domain.Interfaces.Categories;
using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Common.Application.SecurityUtil;

namespace BlogAz.Application.Commands.Blogs.Edit
{
    public class EditBlogCommandHandler : IBaseCommandHandler<EditBlogCommand>
    {
        private readonly IBlogRepository _blogRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBlogCategoryRepository _blogCategoryRepository;
        private readonly ILocalFileService _localFileService;

        public EditBlogCommandHandler(IBlogRepository blogRepository, ICategoryRepository categoryRepository, IBlogCategoryRepository blogCategoryRepository, ILocalFileService localFileService)
        {
            _blogRepository = blogRepository;
            _categoryRepository = categoryRepository;
            _blogCategoryRepository = blogCategoryRepository;
            _localFileService = localFileService;
        }

        public async Task<OperationResult> Handle(EditBlogCommand request, CancellationToken cancellationToken)
        {
            var blog = await _blogRepository.GetTracking(request.Id);
            if (blog == null)
            {
                return OperationResult.Error("Bloq tapılmadı.");
            }
            if (request.Title != blog.Title)
            {
                var blogIsExist = _blogRepository.Exists(q => q.Title == request.Title);
                if (blogIsExist)
                {
                    return OperationResult.Error("Bu adla başqa bir bloq mövcuddur.");
                }
            }
            if (request.ImageFile != null)
            {
                _localFileService.DeleteFile(blog.ImageName, BlogDirectories.BlogImage);

                blog.ImageName = await _localFileService.SaveFileAndGenerateName(request.ImageFile, BlogDirectories.BlogImage);
            }
            blog.Title = request.Title;
            blog.Content = request.Content.SanitizeText();
            blog.UpdatedAt = DateTime.Now;
            await _blogRepository.Save();

            var blogCategoryList = new List<BlogCategory>();
            if (request.CategoryIds != null && request.CategoryIds.Any())
            {
                var blogCategories = await _blogCategoryRepository.GetBlogCategoriesByBlogIdAsync(request.Id);
                _blogCategoryRepository.RemoveRange(blogCategories);

                foreach (var item in request.CategoryIds)
                {
                    var blogCategory = new BlogCategory
                    {
                        BlogId = blog.Id,
                        CategoryId = item
                    };
                    blogCategoryList.Add(blogCategory);
                }
                await _blogCategoryRepository.AddRange(blogCategoryList);
                await _blogCategoryRepository.Save();
            }
            return OperationResult.Success();
        }
    }
}
