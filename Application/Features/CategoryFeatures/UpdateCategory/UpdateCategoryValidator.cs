using FluentValidation;

namespace Application.Features.UpdateCategory
{
    public sealed class UpdateCategoryValidator: AbstractValidator<UpdateCategoryRequest>
    {
        public UpdateCategoryValidator()
        {
            RuleFor(x => x.ItemId).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.IsDefault).NotNull();
            RuleFor(x => x.IsExpense).NotNull();
        }
    }
}
