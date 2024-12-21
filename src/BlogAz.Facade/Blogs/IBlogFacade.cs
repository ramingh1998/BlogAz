using BlogAz.Application.Commands.Blogs.Add;
using BlogAz.Application.Commands.Blogs.Delete;
using BlogAz.Application.Commands.Blogs.Edit;
using BlogAz.Application.DTOs.Blogs;
using Common.Application;

namespace BlogAz.Facade.Blogs
{
    public interface IBlogFacade
    {
        Task<OperationResult> AddBlogAsync(AddBlogCommand command);
        Task<OperationResult> EditBlogAsync(EditBlogCommand command);
        Task<OperationResult> DeleteBlogAsync(DeleteBlogCommand command);
        Task<BlogFilterResult> GetBlogsByFilterAsync(BlogFilterParams filterParams);
        Task<BlogDto> GetBlogByIdAsync(long id);
    }
}
