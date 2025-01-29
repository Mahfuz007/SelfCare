using Application.Common;
using Application.Repositories;
using FluentValidation;
using MediatR;

namespace Application.Features.Investments.UpdatePayment
{
    public class UpdatePaymentHandler : IRequestHandler<UpdatePaymentRequest, CommonResponse>
    {
        private readonly IValidator<UpdatePaymentRequest> _validator;
        private readonly IInvestmentRepository _repository;

        public UpdatePaymentHandler(IValidator<UpdatePaymentRequest> validator, IInvestmentRepository repository)
        {
            _validator = validator;
            _repository = repository;
        }

        public async Task<CommonResponse> Handle(UpdatePaymentRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return new CommonResponse(validationResult);
            }

            return await _repository.UpdatePaymentAsync(request);
        }
    }
}
