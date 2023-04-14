using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CategoryFeatures.CreateCategory
{
    public sealed class CreateCategoryValidator: AbstractValidator<CreateCategoryRequest>
    {
        public CreateCategoryValidator()
        {
            RuleFor(x => x.category.Name).NotEmpty().MinimumLength(3).MaximumLength(20);
            RuleFor(x => x.category.ItemId).NotEmpty();
            RuleFor(x => x.category.IsExpense).NotEmpty();
            RuleFor(x => x.category.IsDefault).NotEmpty();
        }
    }
}
