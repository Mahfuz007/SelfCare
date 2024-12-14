using Application.Repositories;
using MediatR;

namespace Application.Features.TaxFeature.GetTaxCalculation
{
    public class GetTaxCalculationHandler : IRequestHandler<GetTaxCalculationRequest, GetTaxCalculationResponse>
    {
        private readonly ITaxRepository _taxRepository;

        public GetTaxCalculationHandler(ITaxRepository taxRepository)
        {
            _taxRepository = taxRepository;
        }

        public Task<GetTaxCalculationResponse> Handle(GetTaxCalculationRequest request, CancellationToken cancellationToken)
        {
            var result = _taxRepository.GetTaxCalculation(request);
            return Task.FromResult(result);
        }
    }
}


