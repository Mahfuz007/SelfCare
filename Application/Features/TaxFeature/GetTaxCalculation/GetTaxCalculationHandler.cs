using Application.Common;
using Application.Repositories;
using MediatR;

namespace Application.Features.TaxFeature.GetTaxCalculation
{
    public class GetTaxCalculationHandler : IRequestHandler<GetTaxCalculationRequest, CommonResponse>
    {
        private readonly ITaxRepository _taxRepository;

        public GetTaxCalculationHandler(ITaxRepository taxRepository)
        {
            _taxRepository = taxRepository;
        }

        public Task<CommonResponse> Handle(GetTaxCalculationRequest request, CancellationToken cancellationToken)
        {
            var result = _taxRepository.GetTaxCalculation(request);
            return Task.FromResult(result);
        }
    }
}


