using Application.Common;
using Application.Features.Users.UserSignUp;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("User")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<CommonResponse> SignUp([FromBody]UserSignUpRequest request)
        {
            return await _mediator.Send(request);
        }
    }
}
