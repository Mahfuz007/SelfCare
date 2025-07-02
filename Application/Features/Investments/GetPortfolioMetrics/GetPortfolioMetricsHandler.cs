using Application.Common;
using Application.Repositories;
using MediatR;

namespace Application.Features.Investments.GetPortfolioMetrics
{
    public class GetPortfolioMetricsHandler : IRequestHandler<GetPortfolioMetricsRequest, CommonResponse>
    {
        private readonly IInvestmentRepository _repository;

        public GetPortfolioMetricsHandler(IInvestmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<CommonResponse> Handle(GetPortfolioMetricsRequest request, CancellationToken cancellationToken)
        {
            var portfolioMetrics = await _repository.GetPortfolioMetrics(request);
            return portfolioMetrics;
        }
    }
} 