﻿using Application.Common;
using MediatR;

namespace Application.Features.ExpenseFeatures.GetExpense
{
    public class GetExpenseRequest : QueryRequestBase, IRequest<List<GetExpenseResponse>>
    {
        public string ExpenseId { get; set; } = string.Empty;
        public string ExpenseName { get; set;} = string.Empty;
    }
}
