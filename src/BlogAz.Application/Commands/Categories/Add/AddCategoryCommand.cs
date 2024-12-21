using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Application;

namespace BlogAz.Application.Commands.Categories.Add
{
    public record AddCategoryCommand(string Name, long? ParentId) : IBaseCommand;
}
