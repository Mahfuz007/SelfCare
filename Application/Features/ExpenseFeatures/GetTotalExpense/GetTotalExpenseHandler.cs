using Application.Repositories;
using MediatR;

namespace Application.Features.ExpenseFeatures.GetTotalExpense
{
    public class GetTotalExpenseHandler : IRequestHandler<GetTotalExpenseRequest, GetTotalExpenseResponse>
    {
        private readonly IExpenseRepository _expenseRepository;

        public GetTotalExpenseHandler(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task<GetTotalExpenseResponse> Handle(GetTotalExpenseRequest request, CancellationToken cancellationToken)
        {
            return await _expenseRepository.GetTotalExpense(request);
        }
    }
}
