using Application.Common;
using MediatR;

namespace Application.Features.Investments.GetPortfolioMetrics
{
    public class GetPortfolioMetricsRequest : QueryRequestBase, IRequest<CommonResponse>
    {
        public string UserId { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? InvestmentType { get; set; }
        public bool IncludeInactive { get; set; } = false;
    }
} 