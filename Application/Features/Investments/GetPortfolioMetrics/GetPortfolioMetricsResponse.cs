namespace Application.Features.Investments.GetPortfolioMetrics
{
    public class GetPortfolioMetricsResponse
    {
        public double TotalInvestedAmount { get; set; }
        public double CurrentInvestedAmount { get; set; }
        public double EarnedAmount { get; set; }
        public int CurrentInvestedItem { get; set; }
        public int TotalInvestedItem { get; set; }
    }
} 