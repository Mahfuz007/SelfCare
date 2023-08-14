using Application.Features.CategoryFeatures.CreateCategory;
using Application.Features.CategoryFeatures.DeleteCategory;
using Application.Features.CategoryFeatures.GetAllCategory;
using Application.Features.CategoryFeatures.GetCategory;
using Application.Features.ExpenseFeatures.AddExpense;
using Application.Features.ExpenseFeatures.DeleteExpense;
using Application.Features.ExpenseFeatures.GetAllExpense;
using Application.Features.ExpenseFeatures.GetExpense;
using Application.Features.ExpenseFeatures.GetTotalExpense;
using Application.Features.ExpenseFeatures.UpdateExpense;
using Application.Features.UpdateCategory;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("Expense")]
    public class ExpenseController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public ExpenseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Category
        [HttpPost("CreateCategory")]
        public async Task<ActionResult<CreateCategoryResponse>> CreateCategory([FromBody] CreateCategoryRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpPost("UpdateCategory")]
        public async Task<ActionResult<UpdateCategoryResponse>> UpdateCategory([FromBody] UpdateCategoryRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet("GetCategory")]
        public async Task<ActionResult<GetCategoryResponse>> GetCategory([FromQuery] GetCategoryRequest request, CancellationToken cancellation)
        {
            return await _mediator.Send(request, cancellation);
        }

        [HttpGet("GetAllCategory")]
        public async Task<ActionResult<List<GetAllCategoryResponse>>> GetAllCategory([FromQuery] GetAllCategoryRequest request, CancellationToken cancellationToken)
        {
            return await _mediator.Send(request, cancellationToken);
        }

        [HttpDelete("DeleteCategory")]
        public async Task<ActionResult<bool>> DeleteCategory([FromQuery] DeleteCategoryRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        #endregion
        #region Expense
        [HttpPost("AddExpense")]
        public async Task<ActionResult<AddExpenseResponse>> AddExpense([FromBody] AddExpenseRequest requst, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(requst, cancellationToken);
            return Ok(response);
        }

        [HttpGet("GetAllExpense")]
        public async Task<ActionResult<List<GetAllExpenseResponse>>> GetAllExpense([FromQuery] GetAllExpenseRequest request,CancellationToken cancellationToken1)
        {
            var response = await _mediator.Send(request, cancellationToken1);
            return Ok(response);
        }

        [HttpPut("UpdateExpense")]
        public async Task<ActionResult<UpdateExpenseResponse>> UpdateExpense([FromBody] UpdateExpenseRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet("GetExpense")]
        public async Task<ActionResult<GetExpenseRequest>> GetExpense([FromQuery] GetExpenseRequest request,CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpDelete("DeleteExpense")]
        public async Task<ActionResult<bool>> DeleteExpense([FromQuery] DeleteExpenseRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet("GetTotalExpenseAmount")]
        public async Task<ActionResult<GetTotalExpenseResponse>> GetTotalExpenseAmount([FromQuery] GetTotalExpenseRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }
        #endregion
    }
}
