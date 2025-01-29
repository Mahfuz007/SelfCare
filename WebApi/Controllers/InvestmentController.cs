﻿using Application.Common;
using Application.Features.Investments.Initiate;
using Application.Features.Investments.UpdatePayment;
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

        [HttpPost]
        public async Task<CommonResponse> Initiate([FromBody] InitiateInvestmentRequest request)
        {
            return await _sender.Send<CommonResponse>(request);
        }

        [HttpPut("update-payment")]
        public async Task<CommonResponse> UpdatePayment([FromBody] UpdatePaymentRequest request)
        {
            return await _sender.Send<CommonResponse>(request);
        }
    }
}
