using Application.Common;
using Application.Repositories;
using FluentValidation;
using MediatR;

namespace Application.Features.Investments.AddPurchaseInfo
{
    public class AddPurchaseInfoHandler : IRequestHandler<AddPurchaseInfoRequest, CommonResponse>
    {
        private readonly IValidator<AddPurchaseInfoRequest> _validator;
        private readonly IInvestmentRepository _repository;

        public AddPurchaseInfoHandler(IValidator<AddPurchaseInfoRequest> validator, IInvestmentRepository repository)
        {
            _validator = validator;
            _repository = repository;
        }

        public async Task<CommonResponse> Handle(AddPurchaseInfoRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return new CommonResponse(validationResult);
            }

            return await _repository.AddPurchaseInfo(request);
        }
    }
}
