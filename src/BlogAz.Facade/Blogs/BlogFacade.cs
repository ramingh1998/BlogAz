using BlogAz.Application.Commands.Blogs.Add;
using BlogAz.Application.Commands.Blogs.Delete;
using BlogAz.Application.Commands.Blogs.Edit;
using BlogAz.Application.DTOs.Blogs;
using BlogAz.Application.Queries.Blogs.GetByFilter;
using BlogAz.Application.Queries.Blogs.GetById;
using Common.Application;
using MediatR;

namespace BlogAz.Facade.Blogs
{
    public class BlogFacade : IBlogFacade
    {
        private readonly IMediator _mediator;

        public BlogFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OperationResult> AddBlogAsync(AddBlogCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> DeleteBlogAsync(DeleteBlogCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> EditBlogAsync(EditBlogCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<BlogDto> GetBlogByIdAsync(long id)
        {
            return await _mediator.Send(new GetBlogByIdQuery(id));
        }

        public async Task<BlogFilterResult> GetBlogsByFilterAsync(BlogFilterParams filterParams)
        {
            return await _mediator.Send(new GetBlogsByFilterQuery(filterParams));
        }
    }
}
