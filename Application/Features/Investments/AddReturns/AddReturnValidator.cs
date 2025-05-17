using Application.Constants;
using Application.Features.Investments.AddProfits;
using Application.Repositories;
using FluentValidation;

namespace Application.Features.Investments.AddReturns
{
    public class AddReturnValidator : AbstractValidator<AddReturnRequest>
    {
        private readonly IInvestmentRepository _repository;
        public AddReturnValidator(IInvestmentRepository repository) 
        { 
            _repository = repository;
            RuleFor(x => x.InvestmentId)
                .NotEmpty()
                .NotNull()
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
