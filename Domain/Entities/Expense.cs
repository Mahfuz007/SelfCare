using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [BsonCollection("Expense")]
    public sealed class Expense : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public double Amount { get; set; }
        public string CategoryName { get; set; }
        public string CategoryId { get; set; }
    }
}
