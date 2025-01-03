using Application.Common;
using MediatR;

namespace Application.Features.TaxFeature.GetTaxCalculation
{
    public sealed record GetTaxCalculationRequest : IRequest<CommonResponse>
    {
        public long TotalIncome { get; set; }
        public long TotalInvestment { get; set; }
    }
}
