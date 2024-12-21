using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace BlogAz.Application.Commands.Blogs.Edit
{
    public class EditBlogCommandValidator : AbstractValidator<EditBlogCommand>
    {
        public EditBlogCommandValidator()
        {
            RuleFor(q => q.Title).MaximumLength(60).WithMessage("Ad 60 simvoldan artıq olmamalıdır.").NotEmpty().WithMessage("Ad mütləqdir.").NotNull().WithMessage("Ad mütləqdir.");
            RuleFor(q => q.ImageName).NotEmpty().WithMessage("Şəkil mütləqdir.").NotNull().WithMessage("Şəkil mütləqdir.");
            RuleFor(q => q.Content).MaximumLength(600).WithMessage("Məzmun 600 simvoldan artıq olmamalıdır.").NotEmpty().WithMessage("Məzmun mütləqdir.").NotNull().WithMessage("Məzmun mütləqdir.");
        }
    }
}
