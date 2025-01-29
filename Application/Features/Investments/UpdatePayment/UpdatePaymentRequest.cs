using Application.Common;
using MediatR;

namespace Application.Features.Investments.UpdatePayment
{
    public record UpdatePaymentRequest : IRequest<CommonResponse>
    {
        public string InvestmentId { get; set; } = string.Empty;
        public string Method { get; set; } = string.Empty;
        public string Way {  get; set; } = string.Empty;
        public double Amount { get; set; }
        public string SenderName { get; set; } = string.Empty;
        public string SenderAccountNumber {  get; set; } = string.Empty;
        public string SenderBranchName {  get; set; } = string.Empty;
        public string ReceiverName {  get; set; } = string.Empty;
        public string ReceiverAccountNumber { get; set; } = string.Empty;
        public string ReceiverBranchName { get; set;} = string.Empty;
    }
}
