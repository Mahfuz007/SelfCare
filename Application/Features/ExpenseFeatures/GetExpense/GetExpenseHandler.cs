using Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ExpenseFeatures.GetExpense
{
    public class GetExpenseHandler : IRequestHandler<GetExpenseRequest, GetExpenseResponse>
    {
        private IExpenseRepository _expenseRepository;

        public GetExpenseHandler(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task<GetExpenseResponse> Handle(GetExpenseRequest request, CancellationToken cancellationToken)
        {
            return await _expenseRepository.GetExpenseById(request.ExpenseId);
        }
    }
}
