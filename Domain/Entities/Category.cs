using Domain.Common;

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
