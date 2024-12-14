using Application.Features.TaxFeature.GetTaxCalculation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("Tax")]
    public class TaxController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaxController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<GetTaxCalculationResponse>> TaxCalculation([FromBody] GetTaxCalculationRequest request, CancellationToken cancellation)
        {
            return await _mediator.Send(request, cancellation);
        }
    }
}
