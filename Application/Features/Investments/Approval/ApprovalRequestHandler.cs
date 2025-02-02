using Application.Common;
using Application.Repositories;
using FluentValidation;
using MediatR;

namespace Application.Features.Investments.Approval
{
    public class ApprovalRequestHandler : IRequestHandler<ApprovalRequest, CommonResponse>
    {
        private readonly IInvestmentRepository _investmentRepository;
        private readonly IValidator<ApprovalRequest> _validator;

        public ApprovalRequestHandler(IInvestmentRepository investmentRepository, IValidator<ApprovalRequest> validator)
        {
            _investmentRepository = investmentRepository;
            _validator = validator;
        }

        public async Task<CommonResponse> Handle(ApprovalRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if(!validationResult.IsValid)
            {
                return new CommonResponse(validationResult);
            }

            var result = await _investmentRepository.UpdateApprovalAsync(request);
            return result;
        }
    }
}
