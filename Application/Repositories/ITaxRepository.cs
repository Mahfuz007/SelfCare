using Application.Features.TaxFeature.GetTaxCalculation;

namespace Application.Repositories
{
    public interface ITaxRepository
    {
        GetTaxCalculationResponse GetTaxCalculation(GetTaxCalculationRequest request);
    }
}
