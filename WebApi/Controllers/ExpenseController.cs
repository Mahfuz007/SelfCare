using Application.Features.CategoryFeatures.CreateCategory;
using Application.Features.CategoryFeatures.DeleteCategory;
using Application.Features.CategoryFeatures.GetAllCategory;
using Application.Features.CategoryFeatures.GetCategory;
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
    }
}
