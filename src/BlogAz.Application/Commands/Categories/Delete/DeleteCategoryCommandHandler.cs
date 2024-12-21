using BlogAz.Domain.Interfaces.Categories;
using Common.Application;

namespace BlogAz.Application.Commands.Categories.Delete
{
    public class DeleteCategoryCommandHandler : IBaseCommandHandler<DeleteCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;

        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<OperationResult> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetTracking(request.Id);
            var hasSubCategories = await _categoryRepository.ExistsAsync(x => x.ParentId == request.Id);
            if (category == null)
            {
                return OperationResult.NotFound();
            }
            if (hasSubCategories)
            {
                return OperationResult.Error("Bu kateqoriyanın alt kateqoriyaları var və silinə bilməz.");
            }
            _categoryRepository.Remove(category);
            await _categoryRepository.Save();
            return OperationResult.Success();
        }
    }
}
