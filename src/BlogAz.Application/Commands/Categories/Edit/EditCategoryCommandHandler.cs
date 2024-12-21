using BlogAz.Domain.Interfaces.Categories;
using Common.Application;

namespace BlogAz.Application.Commands.Categories.Edit
{
    public class EditCategoryCommandHandler : IBaseCommandHandler<EditCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;

        public EditCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<OperationResult> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetTracking(request.CategoryId);
            if (category == null)
            {
                return OperationResult.Error("Kateqoriya tapılmadı.");
            }
            if (category.Name != request.Name)
            {
                if (await _categoryRepository.ExistsAsync(q => q.Name == request.Name))
                {
                    return OperationResult.Error("Bu ad ilə artıq bir kateqoriya mövcuddur.");
                }
            }
            category.Name = request.Name;
            category.ParentId = request.ParentId;
            _categoryRepository.Update(category);
            return OperationResult.Success();
        }
    }
}
