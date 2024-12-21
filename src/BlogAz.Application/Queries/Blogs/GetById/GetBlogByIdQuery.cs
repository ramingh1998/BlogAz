using BlogAz.Application.DTOs.Blogs;
using Common.Query;

namespace BlogAz.Application.Queries.Blogs.GetById
{
    public record GetBlogByIdQuery(long Id) : IQuery<BlogDto>;
}
