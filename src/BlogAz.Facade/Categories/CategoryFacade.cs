using BlogAz.Application.Commands.Categories.Add;
using BlogAz.Application.Commands.Categories.Delete;
using BlogAz.Application.Commands.Categories.Edit;
using BlogAz.Application.DTOs.Categories;
using BlogAz.Application.Queries.Categories.GetById;
using BlogAz.Application.Queries.Categories.GetList;
using Common.Application;
using MediatR;

namespace BlogAz.Facade.Categories
{
    public class CategoryFacade : ICategoryFacade
    {
        private readonly IMediator _mediator;

        public CategoryFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OperationResult> AddCategoryAsync(AddCategoryCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> DeleteCategoryAsync(DeleteCategoryCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> EditCategoryAsync(EditCategoryCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<List<CategoryDto>> GetAllCategoriesAsync()
        {
            return await _mediator.Send(new GetAllCategoriesQuery());
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(long id)
        {
            return await _mediator.Send(new GetCategoryByIdQuery(id));
        }
    }
}
