using Application.Repositories;
using MediatR;

namespace Application.Features.ExpenseFeatures.GetExpense
{
    public class GetExpenseHandler : IRequestHandler<GetExpenseRequest, List<GetExpenseResponse>>
    {
        private readonly IExpenseRepository _expenseRepository;

        public GetExpenseHandler(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task<List<GetExpenseResponse>> Handle(GetExpenseRequest request, CancellationToken cancellationToken)
        {
            return await _expenseRepository.GetExpenses(request);
        }
    }
}
