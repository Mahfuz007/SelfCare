using Application.Common;
using Application.Repositories;
using MediatR;

namespace Application.Features.ExpenseFeatures.GetTotalExpense
{
    public class GetTotalExpenseHandler : IRequestHandler<GetTotalExpenseRequest, CommonResponse>
    {
        private readonly IExpenseRepository _expenseRepository;

        public GetTotalExpenseHandler(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task<CommonResponse> Handle(GetTotalExpenseRequest request, CancellationToken cancellationToken)
        {
            return await _expenseRepository.GetTotalExpenseAmount(request);
        }
    }
}
