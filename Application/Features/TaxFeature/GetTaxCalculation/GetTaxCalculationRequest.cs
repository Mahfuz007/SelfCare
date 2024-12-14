using MediatR;

namespace Application.Features.TaxFeature.GetTaxCalculation
{
    public sealed record GetTaxCalculationRequest : IRequest<GetTaxCalculationResponse>
    {
        public long TotalIncome { get; set; }
        public long TotalInvestment { get; set; }
    }
}
