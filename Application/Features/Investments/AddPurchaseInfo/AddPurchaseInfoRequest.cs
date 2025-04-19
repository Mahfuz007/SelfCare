using Application.Common;
using MediatR;

namespace Application.Features.Investments.AddPurchaseInfo
{
    public class AddPurchaseInfoRequest : IRequest<CommonResponse>
    {
        public string InvestmentId { get; set; } = string.Empty;
        public int UnitCount { get; set; }
        public double UnitPrice { get; set; }
        public double Amount { get; set; }
        public double Charge { get; set; }
        public string InvoiceNo { get; set; } = string.Empty;
        public string Remarks { get; set; } = string.Empty;

    }
}
