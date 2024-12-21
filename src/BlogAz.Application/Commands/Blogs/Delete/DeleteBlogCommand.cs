using Common.Application;

namespace BlogAz.Application.Commands.Blogs.Delete
{
    public record DeleteBlogCommand(long Id) : IBaseCommand;
}
