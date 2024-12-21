using Common.Application;
using Microsoft.AspNetCore.Http;

namespace BlogAz.Application.Commands.Blogs.Add
{
    public class AddBlogCommand : IBaseCommand
    {
        public string Title { get; set; }
        public IFormFile ImageFile { get; set; }
        public string Content { get; set; }
        public List<long> CategoryIds { get; set; }
    }
}
