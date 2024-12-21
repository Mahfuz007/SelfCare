using Application.Features.ExpenseFeatures.AddExpense;
using MediatR;

namespace Application.Features.ExpenseFeatures.UpdateExpense
{
    public class UpdateExpenseRequest : IRequest<UpdateExpenseResponse>
    {
        public string ExpenseId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Amount { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string CategoryId { get; set; } = string.Empty;
    }
}
