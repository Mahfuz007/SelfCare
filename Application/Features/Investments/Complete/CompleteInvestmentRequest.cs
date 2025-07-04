using Application.Common;
using MediatR;

namespace Application.Features.Investments.Complete
{
    public record CompleteInvestmentRequest : IRequest<CommonResponse>
    {
        public string InvestmentId { get; set; } = string.Empty;
        public string Remarks { get; set; } = string.Empty;
    }
} 