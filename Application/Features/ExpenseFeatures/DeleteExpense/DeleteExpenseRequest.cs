using MediatR;

namespace Application.Features.ExpenseFeatures.DeleteExpense
{
    public class DeleteExpenseRequest : IRequest<bool>
    {
        public string ExpenseId { get; set; } = string.Empty;
    }
}
