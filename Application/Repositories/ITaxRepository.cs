using Application.Common;
using Application.Features.TaxFeature.GetTaxCalculation;

namespace Application.Repositories
{
    public interface ITaxRepository
    {
        CommonResponse GetTaxCalculation(GetTaxCalculationRequest request);
    }
}
