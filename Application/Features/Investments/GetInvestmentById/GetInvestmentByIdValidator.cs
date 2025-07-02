using FluentValidation;

namespace Application.Features.Investments.GetInvestmentById
{
    public class GetInvestmentByIdValidator : AbstractValidator<GetInvestmentByIdRequest>
    {
        public GetInvestmentByIdValidator()
        {
            RuleFor(x => x.InvestmentId)
                .NotEmpty()
                .WithMessage("InvestmentId is required")
                .MaximumLength(50)
                .WithMessage("InvestmentId cannot exceed 50 characters");
        }
    }
} 