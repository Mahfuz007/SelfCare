namespace Application.Features.ExpenseFeatures.AddExpense
{
    public record AddExpenseResponse
    {
        public string ItemId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Amount { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string CategoryId { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
