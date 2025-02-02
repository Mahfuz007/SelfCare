using Application.Repositories;
using FluentValidation;

namespace Application.Features.Investments.Approval
{
    public class ApprovalValidator : AbstractValidator<ApprovalRequest>
    {
        private readonly IInvestmentRepository _repository;
        public ApprovalValidator(IInvestmentRepository repository)
        {
            _repository = repository;
            RuleFor(x => x.InvestmentId)
                .NotEmpty()
                .MustAsync(BeAValidInvenstmentId);
            RuleFor(x => x.InvoiceNo).NotEmpty();
            RuleFor(x => x.MatureDate).NotEqual(DateTime.MinValue);
        }

        private async Task<bool> BeAValidInvenstmentId(string investmentId, CancellationToken cancellationToken)
        {
            return await _repository.IsExistsAsync(investmentId);
        }
    }
}
