using Application.Common;
using MediatR;

namespace Application.Features.Investments.Approval
{
    public record ApprovalRequest : IRequest<CommonResponse>
    {
        public string InvestmentId { get; set; } = string.Empty;
        public string Remarks { get; set; } = string.Empty;
        public string InvoiceNo { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime MatureDate { get; set; }
    }
}
