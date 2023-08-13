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
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Amount).NotEmpty().GreaterThanOrEqualTo(1).WithMessage("Amount can not be 0 or negetive");
            RuleFor(x => x.CategoryId).NotEmpty().MustAsync(this.checkIfCategoryExists).WithMessage("No category Exists with the given category Id");
            RuleFor(x => x.CategoryName).NotEmpty();
        }

        private async Task<bool> checkIfCategoryExists(string categoryId, CancellationToken cancellationToken)
        {
            return await _categoryRepository.BeAnExistingCategory(categoryId);
        }
    }
}
