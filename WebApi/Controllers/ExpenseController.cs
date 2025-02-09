﻿using Application.Common;
using Application.Common.Interfaces;
using Application.Features.CategoryFeatures.CreateCategory;
using Application.Features.CategoryFeatures.DeleteCategory;
using Application.Features.CategoryFeatures.GetCategory;
using Application.Features.CategoryFeatures.UpdateCategory;
using Application.Features.ExpenseFeatures.AddExpense;
using Application.Features.ExpenseFeatures.DeleteExpense;
using Application.Features.ExpenseFeatures.GetExpense;
using Application.Features.ExpenseFeatures.GetTotalExpense;
using Application.Features.ExpenseFeatures.ImportFromExcel;
using Application.Features.ExpenseFeatures.UpdateExpense;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("Expense")]
    public class ExpenseController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IServiceClient _serviceClient;

        public ExpenseController(IMediator mediator, IServiceClient serviceClient)
        {
            _mediator = mediator;
            _serviceClient = serviceClient;
        }

        #region Category
        [HttpPost("Category")]
        public async Task<CommonResponse> CreateCategory([FromBody] CreateCategoryRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return response;
        }

        [HttpPut("Category")]
        public async Task<CommonResponse> UpdateCategory([FromBody] UpdateCategoryRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return response;
        }

        [HttpGet("Category")]
        public async Task<CommonResponse> GetCategory([FromQuery] GetCategoryRequest request, CancellationToken cancellation)
        {
            return await _mediator.Send(request, cancellation);
        }

        [HttpDelete("Category")]
        public async Task<CommonResponse> DeleteCategory([FromQuery] DeleteCategoryRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return result;
        }

        #endregion
        #region Expense
        [HttpPost()]
        public async Task<CommonResponse> AddExpense([FromBody] AddExpenseRequest requst, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(requst, cancellationToken);
            return response;
        }

        [HttpPut()]
        public async Task<CommonResponse> UpdateExpense([FromBody] UpdateExpenseRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return response;
        }

        [HttpGet()]
        public async Task<CommonResponse> GetExpense([FromQuery] GetExpenseRequest request,CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return response;
        }

        [HttpDelete()]
        public async Task<CommonResponse> DeleteExpense([FromQuery] DeleteExpenseRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return response;
        }

        [HttpGet("Summery")]
        public async Task<CommonResponse> GetExpenseSummery([FromQuery] GetExpenseSummeryRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return response;
        }

        [HttpPost("ImportFromExcel")]
        [Consumes("multipart/form-data")]
        public async Task<CommonResponse> UploadExcelFile([FromForm] ImportExpenseExcelRequest request, CancellationToken cancellationToken)
        {
            if (request == null || request.File == null) return new CommonResponse(System.Net.HttpStatusCode.BadRequest, "Provide Valid Excel File");
            var response = await _mediator.Send(request, cancellationToken);
            return response;

        }
        #endregion
    }
}
