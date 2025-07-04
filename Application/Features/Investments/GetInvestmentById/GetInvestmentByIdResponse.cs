using Domain.Entities;

namespace Application.Features.Investments.GetInvestmentById
{
    public class GetInvestmentByIdResponse
    {
        public string InvestmentId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string SourceName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public double Amount { get; set; }
        public int UnitCount { get; set; }
        public double UnitPrice { get; set; }
        public int DurationInMonths { get; set; }
        public double MaximumRoiDeclaredInPercentage { get; set; }
        public int ReturnInstallmentCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? ExpectedMatureDate { get; set; }
        public bool IsPaymentCompleted { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public ConfirmationDetails? ConfirmationDetails { get; set; }
        public List<PurchaseInfo> PurchaseInfos { get; set; } = new();
        public List<ReturnDetails> ReturnInstallmentDetails { get; set; } = new();
        public double FinalProfitAmount { get; set; }
        public double FinalProfitPercentage { get; set; }
        public DateTime? FinalMatureDate { get; set; }
        public double FinalReturnAmount { get; set; }
    }
} 