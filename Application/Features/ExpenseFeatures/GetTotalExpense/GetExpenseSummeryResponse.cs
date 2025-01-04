namespace Application.Features.ExpenseFeatures.GetTotalExpense
{
    public record GetExpenseSummeryResponse
    {
        public double TotalAmount { get; set; }
        public Dictionary<string, double> ExpenseAmountByCategory { get; set; } = new Dictionary<string, double>();
        public Dictionary<string, double> ExpensePercentageByCategory { get; set; } = new Dictionary<string, double>();
    }
}
