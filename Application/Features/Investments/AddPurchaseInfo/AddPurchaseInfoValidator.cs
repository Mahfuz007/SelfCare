using Application.Constants;
using Application.Repositories;
using FluentValidation;

namespace Application.Features.Investments.AddPurchaseInfo
{
    public class AddPurchaseInfoValidator : AbstractValidator<AddPurchaseInfoRequest>
    {
        private readonly IInvestmentRepository _repository;
        public AddPurchaseInfoValidator(IInvestmentRepository repository) {
            _repository = repository;

            RuleFor(x => x.InvestmentId)
                .NotEmpty()
                .MustAsync(BeValidInvestmentId);
            RuleFor(x => x.Amount).GreaterThan(0);
        }

        private async Task<bool> BeValidInvestmentId(string investmentId, CancellationToken arg2)
        {
            var isExists = await _repository.IsExistsAsync(investmentId);
            var status = await _repository.GetInvestmentStatusAsync(investmentId);
            return isExists 
                && status != InvestmentConstant.Status.CLOSED.ToString() 
                && status != InvestmentConstant.Status.COMPLETED.ToString();
        }
    }
}
