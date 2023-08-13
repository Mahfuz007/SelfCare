using Application.Features.ExpenseFeatures.AddExpense;
using MediatR;

namespace Application.Features.ExpenseFeatures.UpdateExpense
{
    public class UpdateExpenseRequest : IRequest<UpdateExpenseResponse>
    {
        public string ExpenseId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public double Amount { get; set; }
        public string CategoryName { get; set; }
        public string CategoryId { get; set; }
    }
}
