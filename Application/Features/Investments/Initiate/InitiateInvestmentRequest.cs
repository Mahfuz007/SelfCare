using Application.Common;
using MediatR;

namespace Application.Features.Investments.Initiate
{
    public record InitiateInvestmentRequest : IRequest<CommonResponse>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string SourceName { get; set; } = string.Empty;
        public double Amount { get; set; }
        public int DurationInMonths { get; set; }
        public double MaximumRoiDeclaredInPercentage { get; set; }
        public int ReturnInstallmentCount { get; set; }
    }
}
