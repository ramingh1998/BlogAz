using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Query.Filter;

namespace BlogAz.Application.DTOs.Blogs
{
    public class BlogFilterParams : BaseFilterParam
    {
        public string Title { get; set; }
    }
}
