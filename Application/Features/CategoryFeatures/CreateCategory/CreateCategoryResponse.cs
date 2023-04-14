using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CategoryFeatures.CreateCategory
{
    public sealed record CreateCategoryResponse
    {
        public string Name { get; set; }
        public string ItemId { get; set; }
        public bool IsDefault { get; set; }
        public bool IsExpense { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
