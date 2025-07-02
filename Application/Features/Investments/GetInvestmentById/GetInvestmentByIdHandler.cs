using Application.Common;
using Application.Repositories;
using MediatR;
using System.Net;

namespace Application.Features.Investments.GetInvestmentById
{
    public class GetInvestmentByIdHandler : IRequestHandler<GetInvestmentByIdRequest, CommonResponse>
    {
        private readonly IInvestmentRepository _repository;

        public GetInvestmentByIdHandler(IInvestmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<CommonResponse> Handle(GetInvestmentByIdRequest request, CancellationToken cancellationToken)
        {
            var investment = await _repository.GetInvestmentById(request.InvestmentId);
            
            if (investment == null)
            {
                return new CommonResponse(HttpStatusCode.NotFound, "Investment not found");
            }

            return new CommonResponse(investment);
        }
    }
} 