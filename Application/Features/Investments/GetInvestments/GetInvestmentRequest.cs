using Application.Common;
using MediatR;

namespace Application.Features.Investments.GetInvestments
{
    public class GetInvestmentRequest : QueryRequestBase,IRequest<CommonResponse>
    {
        public string CreatedBy { get; set; } = string.Empty;
        public string SearchKey { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
