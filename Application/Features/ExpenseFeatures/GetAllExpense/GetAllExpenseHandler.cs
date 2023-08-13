using Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ExpenseFeatures.GetAllExpense
{
    public sealed class GetAllExpenseHandler : IRequestHandler<GetAllExpenseRequest, List<GetAllExpenseResponse>>
    {
        private readonly IExpenseRepository _expenseRepository;

        public GetAllExpenseHandler(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task<List<GetAllExpenseResponse>> Handle(GetAllExpenseRequest request, CancellationToken cancellationToken)
        {
            return await _expenseRepository.GetAllExpense();
        }
    }
}
