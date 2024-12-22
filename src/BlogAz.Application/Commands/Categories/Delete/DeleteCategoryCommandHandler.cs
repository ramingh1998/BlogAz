using BlogAz.Domain.Interfaces.Blogs;
using BlogAz.Domain.Interfaces.Categories;
using Common.Application;

namespace BlogAz.Application.Commands.Categories.Delete
{
    public class DeleteCategoryCommandHandler : IBaseCommandHandler<DeleteCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBlogCategoryRepository _blogCategoryRepository;

        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, IBlogCategoryRepository blogCategoryRepository)
        {
            _categoryRepository = categoryRepository;
            _blogCategoryRepository = blogCategoryRepository;
        }

        public async Task<OperationResult> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetTracking(request.Id);
            var blogCategory=  await _blogCategoryRepository.ToListAsync(x => x.CategoryId == request.Id);
            var hasSubCategories = await _categoryRepository.ExistsAsync(x => x.ParentId == request.Id);
            if (category == null)
            {
                return OperationResult.NotFound();
            }
            if (hasSubCategories)
            {
                return OperationResult.Error("Bu kateqoriyanın alt kateqoriyaları var və silinə bilməz.");
            }
            if (blogCategory.Any())
            {
                return OperationResult.Error("Bu kateqoriyanın bloqları var və silinə bilməz.");
            }
            _categoryRepository.Remove(category);
            await _categoryRepository.Save();
            return OperationResult.Success();
        }
    }
}
