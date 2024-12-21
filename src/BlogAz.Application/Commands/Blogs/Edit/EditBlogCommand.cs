using Common.Application;
using Microsoft.AspNetCore.Http;

namespace BlogAz.Application.Commands.Blogs.Edit
{
    public class EditBlogCommand : IBaseCommand
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public IFormFile? ImageName { get; set; }
        public string Content { get; set; }
        public List<long> CategoryIds { get; set; }
    }
}
