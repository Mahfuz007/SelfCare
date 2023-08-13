using Amazon.Runtime.Internal;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ExpenseFeatures.AddExpense
{
    public class AddExpenseRequest : IRequest<AddExpenseResponse>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public double Amount { get; set; }
        public string CategoryName { get; set; }
        public string CategoryId { get; set; }
    }
}
