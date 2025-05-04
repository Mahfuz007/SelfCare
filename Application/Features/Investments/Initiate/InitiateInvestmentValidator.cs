using FluentValidation;

namespace Application.Features.Investments.Initiate
{
    public class InitiateInvestmentValidator : AbstractValidator<InitiateInvestmentRequest>
    {
        public InitiateInvestmentValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.DurationInMonths).GreaterThan(0);
            RuleFor(x => x.SourceName).NotEmpty();
            RuleFor(x => x.ReturnInstallmentCount).GreaterThan(0);
            RuleFor(x => x.MaximumRoiDeclaredInPercentage).GreaterThan(0);
        }
    }
}
