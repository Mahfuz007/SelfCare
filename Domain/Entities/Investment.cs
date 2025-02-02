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
        public DateTime ExpectedMatureDate { get; set; } = DateTime.MinValue;
        public DateTime FinalMatureDate { get; set; } = DateTime.MinValue;
        public int DurationInMonths { get; set; } 
        public double MaximumRoiDeclaredInPercentage { get; set; } 
        public double FinalProfitAmount { get; set; }
        public double FinalProfitPercentage { get; set; }
        public double FinalReturnAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public bool IsPaymentCompleted { get; set; }
        public PaymentDetails? SenderPaymentDetails { get; set; }
        public PaymentDetails? ReceiverPaymentDetails { get; set; }
        public int ReturnInstallmentCount { get; set; }
        public List<PaymentDetails>? ReturnInstallmentDetails { get; set; }
        public ConfirmationDetails? ConfirmationDetails { get; set; }
        public DateTime StartDate { get; set; } = DateTime.MinValue;
    }

    public class PaymentDetails
    {
        public double Amount { get; set; }
        public DateTime When { get; set; } 
        public string Method { get; set; } = string.Empty;
        public PaymentMethodDetails MethodDetails { get; set; } = new PaymentMethodDetails();
        public string Description { get; set; } = string.Empty;
        public string TransferType { get; set; } = string.Empty;
    }

    public class PaymentMethodDetails
    {
        public string AccountHolderName {  get; set; } = string.Empty;
        public string AccountNo {  get; set; } = string.Empty;
        public string BranchName {  get; set; } = string.Empty;
    }

    public class ConfirmationDetails
    {
        public DateTime When { get; set; }
        public string Remarks {  get; set; } = string.Empty;
        public string InvoiceNo {  get; set; } = string.Empty;
    }
}
