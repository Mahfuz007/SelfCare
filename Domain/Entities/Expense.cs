using Domain.Common;

namespace Domain.Entities
{
    [BsonCollection("Expense")]
    public sealed class Expense : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty ;
        public double Amount { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string CategoryId { get; set; } = string.Empty;
        public string ImportedExcelName { get; set; } = string.Empty;
    }
}
