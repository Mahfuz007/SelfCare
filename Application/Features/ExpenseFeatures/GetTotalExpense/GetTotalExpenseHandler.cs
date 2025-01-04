using Application.Common;
using Application.Repositories;
using MediatR;

namespace Application.Features.ExpenseFeatures.GetTotalExpense
{
    public class GetTotalExpenseHandler : IRequestHandler<GetExpenseSummeryRequest, CommonResponse>
    {
        private readonly IExpenseRepository _expenseRepository;

        public GetTotalExpenseHandler(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task<CommonResponse> Handle(GetExpenseSummeryRequest request, CancellationToken cancellationToken)
        {
            return await _expenseRepository.GetExpenseSummery(request);
        }
    }
}
