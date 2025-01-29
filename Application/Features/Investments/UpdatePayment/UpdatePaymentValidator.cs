using Application.Constants;
using Application.Repositories;
using FluentValidation;

namespace Application.Features.Investments.UpdatePayment
{
    public class UpdatePaymentValidator : AbstractValidator<UpdatePaymentRequest>
    {
        private readonly IInvestmentRepository _repository;
        public UpdatePaymentValidator(IInvestmentRepository repository) 
        {
            _repository = repository;

            RuleFor(x => x.InvestmentId)
                .NotEmpty()
                .MustAsync(BeValidInvestmentId);
            RuleFor(x => x.Amount).GreaterThan(0);
            RuleFor(x => x.SenderName).NotEmpty();
            RuleFor(x => x.SenderAccountNumber).NotEmpty();
            RuleFor(x => x.ReceiverAccountNumber).NotEmpty();
            RuleFor(x => x.ReceiverName).NotEmpty();
            RuleFor(x => x.Method).NotEmpty();
            RuleFor(x => x.Way).NotEmpty();
        }

        private async Task<bool> BeValidInvestmentId(string investmentId, CancellationToken arg2)
        {
            var isExists = await _repository.IsExistsAsync(investmentId);
            var status = await _repository.GetInvestmentStatusAsync(investmentId);
            return isExists && status == InvestmentConstant.Status.INITIATED.ToString();
        }
    }
}
