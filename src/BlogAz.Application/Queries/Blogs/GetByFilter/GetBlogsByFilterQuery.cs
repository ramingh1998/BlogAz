using BlogAz.Application.DTOs.Blogs;
using Common.Query;

namespace BlogAz.Application.Queries.Blogs.GetByFilter
{
    public class GetBlogsByFilterQuery : QueryFilter<BlogFilterResult, BlogFilterParams>
    {
        public GetBlogsByFilterQuery(BlogFilterParams filterParams) : base(filterParams)
        {
        }
    }
}
