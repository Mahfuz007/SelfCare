using Application.Common;
using Application.Features.Investments.AddProfits;
using Application.Repositories;
using FluentValidation;
using MediatR;

namespace Application.Features.Investments.AddReturns
{
    public class AddReturnHandler : IRequestHandler<AddReturnRequest, CommonResponse>
    {
        private readonly IValidator<AddReturnRequest> _validator;
        private readonly IInvestmentRepository _repository;

        public AddReturnHandler(IValidator<AddReturnRequest> validator, IInvestmentRepository repository)
        {
            _validator = validator;
            _repository = repository;
        }

        public async Task<CommonResponse> Handle(AddReturnRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return new CommonResponse(validationResult);
            }

            return await _repository.AddReturnInfo(request);
        }
    }
}
