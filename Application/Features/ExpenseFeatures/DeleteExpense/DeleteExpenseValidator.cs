using Application.Repositories;
using FluentValidation;

namespace Application.Features.ExpenseFeatures.DeleteExpense
{
    public class DeleteExpenseValidator : AbstractValidator<DeleteExpenseRequest>
    {
        private readonly IExpenseRepository _expenseRepository;
        public DeleteExpenseValidator(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
            RuleFor(x => x.ExpenseId)
                .NotEmpty()
                .MustAsync(async (ExpenseId, _) => await _expenseRepository.CheckIfExpenseExists(ExpenseId))
                .WithMessage("No Expense found with give expense id");
        }
    }
}
