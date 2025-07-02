using Application.Common;
using MediatR;

namespace Application.Features.Investments.GetInvestmentById
{
    public class GetInvestmentByIdRequest : IRequest<CommonResponse>
    {
        public string InvestmentId { get; set; } = string.Empty;
    }
} 