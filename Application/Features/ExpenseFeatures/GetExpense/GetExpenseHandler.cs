using Application.Common;
using Application.Repositories;
using MediatR;

namespace Application.Features.ExpenseFeatures.GetExpense
{
    public class GetExpenseHandler : IRequestHandler<GetExpenseRequest, CommonResponse>
    {
        private readonly IExpenseRepository _expenseRepository;

        public GetExpenseHandler(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task<CommonResponse> Handle(GetExpenseRequest request, CancellationToken cancellationToken)
        {
            var expenses = await _expenseRepository.GetExpenses(request);
            return expenses;
        }
    }
}
