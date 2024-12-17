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
            RuleFor(x => x.CategoryId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().When(x => !string.IsNullOrEmpty(x.CategoryId))
                .MustAsync(async(CategoryId, _) => await _categoryRepository.BeAnExistingCategory(CategoryId))
                .WithMessage("No category Exists with the given category Id").When(x => !string.IsNullOrEmpty(x.CategoryId));
            RuleFor(x => x.CategoryName)
           .Must((model, categoryName) => !string.IsNullOrEmpty(model.CategoryId) || string.IsNullOrEmpty(categoryName))
           .WithMessage("CategoryName  is invalid");
        }
    }
}
