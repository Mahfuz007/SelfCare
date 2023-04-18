using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [BsonCollection("Category")]
    public sealed class Category: BaseEntity
    {
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public bool IsExpense { get; set; }
    }
}
