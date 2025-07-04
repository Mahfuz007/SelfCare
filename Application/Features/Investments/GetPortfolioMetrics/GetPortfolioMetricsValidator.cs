using FluentValidation;

namespace Application.Features.Investments.GetPortfolioMetrics
{
    public class GetPortfolioMetricsValidator : AbstractValidator<GetPortfolioMetricsRequest>
    {
        public GetPortfolioMetricsValidator()
        {
            RuleFor(x => x.StartDate)
                .NotEmpty()
                .WithMessage("StartDate is required")
                .LessThanOrEqualTo(x => x.EndDate)
                .WithMessage("StartDate must be less than or equal to EndDate");

            RuleFor(x => x.EndDate)
                .NotEmpty()
                .WithMessage("EndDate is required")
                .GreaterThanOrEqualTo(x => x.StartDate)
                .WithMessage("EndDate must be greater than or equal to StartDate");

            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(0)
                .WithMessage("PageIndex must be greater than or equal to 0");

            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .LessThanOrEqualTo(1000)
                .WithMessage("PageSize must be between 1 and 1000");
        }
    }
} 