using Application.Common;
using MediatR;

namespace Application.Features.ExpenseFeatures.GetTotalExpense
{
    public class GetExpenseSummeryRequest : IRequest<CommonResponse>
    {
        public string? CreatedBy { get; set; }
        public string? CategoryId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

    }
}
