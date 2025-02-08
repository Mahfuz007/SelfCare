using Application.Common;
using Application.Repositories;
using MediatR;

namespace Application.Features.Investments.GetInvestments
{
    public class GetInvestmentHandler : IRequestHandler<GetInvestmentRequest, CommonResponse>
    {
        private readonly IInvestmentRepository _repository;

        public GetInvestmentHandler(IInvestmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<CommonResponse> Handle(GetInvestmentRequest request, CancellationToken cancellationToken)
        {
            var investments = await _repository.GetInvenstments(request);
            return investments;
        }
    }
}
