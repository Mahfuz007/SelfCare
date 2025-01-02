using Application.Common;
using MediatR;

namespace Application.Features.ExpenseFeatures.DeleteExpense
{
    public class DeleteExpenseRequest : IRequest<CommonResponse>
    {
        public string ExpenseId { get; set; } = string.Empty;
    }
}
