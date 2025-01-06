namespace Application.Features.ExpenseFeatures.GetTotalExpense
{
    public record GetExpenseSummeryResponse
    {
        public double TotalAmount { get; set; }
        public Dictionary<string, SummeryDetails> Details { get; set; } = new Dictionary<string, SummeryDetails>();
    }

    public record SummeryDetails
    {
        public double Total { get; set; }
        public double Percentage { get; set; }
        public double Avarage { get; set; }
        public long Count { get; set; }
        public double HeightAmount { get; set; }
    }
}
