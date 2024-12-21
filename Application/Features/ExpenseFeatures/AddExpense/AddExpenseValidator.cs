using Application.Repositories;
using FluentValidation;

namespace Application.Features.ExpenseFeatures.AddExpense
{
    public sealed class AddExpenseValidator : AbstractValidator<AddExpenseRequest>
    {
        private readonly ICategoryRepository _categoryRepository;
        public AddExpenseValidator(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Amount).NotEmpty().GreaterThanOrEqualTo(1).WithMessage("Amount can not be 0 or negetive");
            RuleFor(x => x.CategoryId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().When(x => !string.IsNullOrEmpty(x.CategoryId))
                .MustAsync(this.checkIfCategoryExists).WithMessage("No category Exists with the given category Id").When(x => !string.IsNullOrEmpty(x.CategoryId));
            RuleFor(x => x.CategoryName)
           .Must((model, categoryName) => !string.IsNullOrEmpty(model.CategoryId)|| string.IsNullOrEmpty(categoryName))
           .WithMessage("CategoryName  is invalid");
        }

        private async Task<bool> checkIfCategoryExists(string categoryId, CancellationToken cancellationToken)
        {
            return await _categoryRepository.BeAnExistingCategory(categoryId);
        }
    }
}
