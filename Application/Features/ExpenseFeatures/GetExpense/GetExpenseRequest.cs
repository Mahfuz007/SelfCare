using Application.Common;
using MediatR;

namespace Application.Features.ExpenseFeatures.GetExpense
{
    public class GetExpenseRequest : QueryRequestBase, IRequest<CommonResponse>
    {
        public string ExpenseId { get; set; } = string.Empty;
        public string ExpenseName { get; set;} = string.Empty;
        public string CategoryName { get; set;} = string.Empty;
        public DateTime StartDate { get; set; } = DateTime.MinValue;
        public DateTime EndDate { get; set; } = DateTime.MinValue;
    }
}
