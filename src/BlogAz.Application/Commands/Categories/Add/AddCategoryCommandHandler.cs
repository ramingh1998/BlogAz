using BlogAz.Domain.Entities.Categories;
using BlogAz.Domain.Interfaces.Categories;
using Common.Application;

namespace BlogAz.Application.Commands.Categories.Add
{
    public class AddCategoryCommandHandler : IBaseCommandHandler<AddCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;

        public AddCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<OperationResult> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            if (await _categoryRepository.ExistsAsync(q => q.Name == request.Name))
            {
                return OperationResult.Error("Bu adla başqa bir Kateqoriya mövcuddur.");
            }
            var category = new Category
            {
                Name = request.Name,
                ParentId = request.ParentId
            };
            _categoryRepository.Add(category);
            await _categoryRepository.Save();
            return OperationResult.Success();
        }
    }
}
