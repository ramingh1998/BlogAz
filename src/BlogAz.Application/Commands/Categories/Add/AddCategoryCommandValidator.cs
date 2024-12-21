using FluentValidation;

namespace BlogAz.Application.Commands.Categories.Add
{
    public class AddCategoryCommandValidator : AbstractValidator<AddCategoryCommand>
    {
        public AddCategoryCommandValidator()
        {
            RuleFor(q => q.Name).MaximumLength(60).WithMessage("Ad 60 simvoldan artıq olmamalıdır.").NotEmpty().WithMessage("Ad mütləqdir.").NotNull().WithMessage("Ad mütləqdir.");
        }
    }
}
