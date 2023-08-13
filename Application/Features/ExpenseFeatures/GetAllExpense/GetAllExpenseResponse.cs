using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ExpenseFeatures.GetAllExpense
{
    public sealed record GetAllExpenseResponse
    {
        public string ItemId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public double Amount { get; set; }
        public string CategoryName { get; set; }
        public string CategoryId { get; set; }
    }
}
