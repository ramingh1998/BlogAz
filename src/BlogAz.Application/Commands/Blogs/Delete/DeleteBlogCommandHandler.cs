using BlogAz.Application.Utilities;
using BlogAz.Domain.Interfaces.Blogs;
using Common.Application;
using Common.Application.FileUtil.Interfaces;

namespace BlogAz.Application.Commands.Blogs.Delete
{
    public class DeleteBlogCommandHandler : IBaseCommandHandler<DeleteBlogCommand>
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IBlogCategoryRepository _blogCategoryRepository;
        private readonly ILocalFileService _localFileService;

        public DeleteBlogCommandHandler(IBlogRepository blogRepository, IBlogCategoryRepository blogCategoryRepository, ILocalFileService localFileService)
        {
            _blogRepository = blogRepository;
            _blogCategoryRepository = blogCategoryRepository;
            _localFileService = localFileService;
        }

        public async Task<OperationResult> Handle(DeleteBlogCommand request, CancellationToken cancellationToken)
        {
            var blog = await _blogRepository.GetTracking(request.Id);

            if (blog == null)
            {
                return OperationResult.NotFound();
            }
            var blogCategories = await _blogCategoryRepository.GetBlogCategoriesByBlogIdAsync(request.Id);
            _blogCategoryRepository.RemoveRange(blogCategories);
            _blogRepository.Remove(blog);
            await _blogRepository.Save();
            _localFileService.DeleteFile(BlogDirectories.BlogImage, blog.ImageName);
            return OperationResult.Success();
        }
    }
}
