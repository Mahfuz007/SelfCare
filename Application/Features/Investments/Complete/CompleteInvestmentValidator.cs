using FluentValidation;
using Application.Repositories;
using Application.Constants;

namespace Application.Features.Investments.Complete
{
    public class CompleteInvestmentValidator : AbstractValidator<CompleteInvestmentRequest>
    {
        private readonly IInvestmentRepository _investmentRepository;
        public CompleteInvestmentValidator(IInvestmentRepository investmentRepository)
        {
            _investmentRepository = investmentRepository;
            RuleFor(x => x.InvestmentId).NotEmpty().WithMessage("Investment ID is required");
            RuleFor(x => x.InvestmentId).MustAsync(async (investmentId, cancellationToken) =>
            {
                var isExists = await _investmentRepository.IsExistsAsync(investmentId);
                var status = await _investmentRepository.GetInvestmentStatusAsync(investmentId);
                var isValidStatus = status == InvestmentConstant.Status.INPROGRESS.ToString() || status == InvestmentConstant.Status.CONFIMED.ToString();
                return isExists && isValidStatus;
            }).WithMessage("Investment not found");
            RuleFor(x => x.Remarks).MaximumLength(500).WithMessage("Remarks cannot exceed 500 characters");
        }
    }
} 