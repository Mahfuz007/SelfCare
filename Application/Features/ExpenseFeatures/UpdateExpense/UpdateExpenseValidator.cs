using Application.Repositories;
using FluentValidation;

namespace Application.Features.ExpenseFeatures.UpdateExpense
{
    public class UpdateExpenseValidator : AbstractValidator<UpdateExpenseRequest>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly ICategoryRepository _categoryRepository;
        public UpdateExpenseValidator(IExpenseRepository expenseRepository, ICategoryRepository categoryRepository)
        {
            _expenseRepository = expenseRepository;
            _categoryRepository = categoryRepository;
            RuleFor(x => x.Name)
                .NotEmpty();
            RuleFor(x => x.ExpenseId)
                .NotEmpty()
                .MustAsync(async (ExpenseId, _) => await _expenseRepository.CheckIfExpenseExists(ExpenseId))
                .WithMessage("No expense exists with the expense Id");
            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage("Amount must be greater than 0");
            RuleFor(x => x.CategoryName)
                .NotEmpty();
            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .MustAsync(async (CategoryId, _) => await _categoryRepository.BeAnExistingCategory(CategoryId))
                .WithMessage("No category Exists with the category ID");
        }
    }
}
