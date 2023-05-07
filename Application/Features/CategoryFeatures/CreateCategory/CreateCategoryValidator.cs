using FluentValidation;

namespace Application.Features.CategoryFeatures.CreateCategory
{
    public sealed class CreateCategoryValidator: AbstractValidator<CreateCategoryRequest>
    {
        public CreateCategoryValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(20);
            RuleFor(x => x.ItemId).NotEmpty();
            RuleFor(x => x.IsExpense).NotEmpty();
            RuleFor(x => x.IsDefault).NotEmpty();
        }
    }
}
