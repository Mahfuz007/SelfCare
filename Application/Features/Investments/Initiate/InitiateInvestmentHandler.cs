using Application.Common;
using Application.Repositories;
using FluentValidation;
using MediatR;

namespace Application.Features.Investments.Initiate
{
    public class InitiateInvestmentHandler : IRequestHandler<InitiateInvestmentRequest, CommonResponse>
    {
        private IValidator<InitiateInvestmentRequest> _validator;
        private readonly IInvestmentRepository _repository;

        public InitiateInvestmentHandler(IValidator<InitiateInvestmentRequest> validator, IInvestmentRepository repository)
        {
            _validator = validator;
            _repository = repository;
        }

        public async Task<CommonResponse> Handle(InitiateInvestmentRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return new CommonResponse(validationResult);
            }

            return await _repository.InitiateAsync(request);
        }
    }
}
