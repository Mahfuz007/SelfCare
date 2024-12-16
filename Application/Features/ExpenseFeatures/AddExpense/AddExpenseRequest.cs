using MediatR;

namespace Application.Features.ExpenseFeatures.AddExpense
{
    public class AddExpenseRequest : IRequest<AddExpenseResponse>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Amount { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string CategoryId { get; set; }  = string.Empty ;
    }
}
