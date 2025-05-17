using Application.Common;
using Application.Features.Investments.AddProfits;
using Application.Features.Investments.AddPurchaseInfo;
using Application.Features.Investments.Approval;
using Application.Features.Investments.GetInvestments;
using Application.Features.Investments.Initiate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("Inventment")]
    public class InvestmentController : ControllerBase
    {
        private readonly ISender _sender;

        public InvestmentController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<CommonResponse> GetInvestments([FromQuery]GetInvestmentRequest request)
        {
            return await _sender.Send(request);
        }

        [HttpPost]
        public async Task<CommonResponse> Initiate([FromBody] InitiateInvestmentRequest request)
        {
            return await _sender.Send<CommonResponse>(request);
        }

        [HttpPut("add-purchase-info")]
        public async Task<CommonResponse> UpdatePayment([FromBody] AddPurchaseInfoRequest request)
        {
            return await _sender.Send<CommonResponse>(request);
        }

        [HttpPut("approval")]
        public async Task<CommonResponse> Approval([FromBody] ApprovalRequest request)
        {
            return await _sender.Send<CommonResponse>(request);
        }

        [HttpPut("add-return")]
        public async Task<CommonResponse> AddReturn([FromBody] AddReturnRequest request)
        {
            return await _sender.Send<CommonResponse>(request);
        }
    }
}
