using Application.Common;
using Application.Repositories;
using FluentValidation;
using MediatR;

namespace Application.Features.Investments.Complete
{
    public class CompleteInvestmentHandler : IRequestHandler<CompleteInvestmentRequest, CommonResponse>
    {
        private readonly IValidator<CompleteInvestmentRequest> _validator;
        private readonly IInvestmentRepository _repository;

        public CompleteInvestmentHandler(IValidator<CompleteInvestmentRequest> validator, IInvestmentRepository repository)
        {
            _validator = validator;
            _repository = repository;
        }

        public async Task<CommonResponse> Handle(CompleteInvestmentRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return new CommonResponse(validationResult);
            }

            return await _repository.CompleteAsync(request);
        }
    }
} 