using Application.Common;
using Application.Repositories;
using FluentValidation;
using MediatR;

namespace Application.Features.Users.UserSignUp
{
    public class UserSignUpHandler : IRequestHandler<UserSignUpRequest, CommonResponse>
    {
        private readonly IValidator<UserSignUpRequest> _validator;
        private readonly IUserRepository _userRepository;

        public UserSignUpHandler(IValidator<UserSignUpRequest> validator, IUserRepository userRepository)
        {
            _validator = validator;
            _userRepository = userRepository;
        }

        public async Task<CommonResponse> Handle(UserSignUpRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return new CommonResponse(validationResult);
            }

            return await _userRepository.CreateUserAsync(request.Email, request.Password);
        }
    }
}
