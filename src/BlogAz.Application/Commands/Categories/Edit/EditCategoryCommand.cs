using Common.Application;

namespace BlogAz.Application.Commands.Categories.Edit
{
    public record EditCategoryCommand(long CategoryId, string Name) : IBaseCommand;
}
