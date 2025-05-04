using Domain.Common;

namespace Domain.Entities
{
    [BsonCollection("Investment")]
    public sealed class Investment : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string SourceName { get; set; } = string.Empty;
        public double Amount { get; set; }
        public int UnitCount { get; set; }
        public DateTime ExpectedMatureDate { get; set; } = DateTime.MinValue;
        public DateTime FinalMatureDate { get; set; } = DateTime.MinValue;
        public int DurationInMonths { get; set; } 
        public double MaximumRoiDeclaredInPercentage { get; set; } 
        public double FinalProfitAmount { get; set; }
        public double FinalProfitPercentage { get; set; }
        public double FinalReturnAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public bool IsPaymentCompleted { get; set; }
        public List<PurchaseInfo> PurchaseInfos { get; set; } = new();
        public int ReturnInstallmentCount { get; set; }
        public List<ReturnDetails> ReturnInstallmentDetails { get; set; } = new();
        public ConfirmationDetails? ConfirmationDetails { get; set; }
        public DateTime StartDate { get; set; } = DateTime.MinValue;
        public string PreviousInvestmentId { get; set; } = string.Empty; 
    }

    public class ConfirmationDetails
    {
        public DateTime When { get; set; }
        public string Remarks {  get; set; } = string.Empty;
        public string InvoiceNo {  get; set; } = string.Empty;
    }

    public class PurchaseInfo
    {
        public DateTime When { get; set; }
        public double UnitPrice { get; set; }
        public int UnitCount { get; set; }
        public double Amount { get; set; }
        public double Charge { get; set; }
        public string Remarks { get; set; } = string.Empty;
        public string InvoiceNo { get; set;} = string.Empty;
    }

    public class ReturnDetails
    {
        public DateTime When { get; set; }
        public string Remarks { get; set; } = string.Empty;
        public double Amount { get; set; }
        public string FiscalYear { get; set; } = string.Empty;
    }

}
