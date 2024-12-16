namespace Application.Features.ExpenseFeatures.GetExpense
{
    public record GetExpenseResponse
    {
        public string ItemId { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public double Amount { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string CategoryId { get; set; } = string.Empty;
    }
}
