using Application.Common;
using MediatR;

namespace Application.Features.Investments.AddProfits
{
    public class AddReturnRequest : IRequest<CommonResponse>
    {
        public string InvestmentId { get; set; } = string.Empty;
        public double Amount { get; set; }
        public string Remarks { get; set; }
        public string year { get; set; }
    }
}
