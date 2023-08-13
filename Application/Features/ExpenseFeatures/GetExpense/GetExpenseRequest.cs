using Amazon.Runtime.Internal;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ExpenseFeatures.GetExpense
{
    public class GetExpenseRequest : IRequest<GetExpenseResponse>
    {
        public string ExpenseId { get; set; }
    }
}
