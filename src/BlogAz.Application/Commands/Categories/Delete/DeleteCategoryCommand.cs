using Common.Application;

namespace BlogAz.Application.Commands.Categories.Delete
{
    public record DeleteCategoryCommand(long Id) : IBaseCommand;
}
