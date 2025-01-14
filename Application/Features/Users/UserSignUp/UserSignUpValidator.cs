using Application.Repositories;
using FluentValidation;

namespace Application.Features.Users.UserSignUp
{
    public class UserSignUpValidator : AbstractValidator<UserSignUpRequest>
    {
        private readonly IUserRepository _userRepository;
        public UserSignUpValidator(IUserRepository userRepository) 
        { 
            _userRepository = userRepository;
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is a Mandatory Field")
                .EmailAddress()
                .WithMessage("Should a valid email address")
                .MustAsync(async (email, _) => !await _userRepository.IsUserExists(email))
                .WithMessage("User Already exists");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is a Mandatory Field");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .WithMessage("ConfirmPassword is a Mandatory Field");

            RuleFor(x => x)
                .Must(x => x.Password == x.ConfirmPassword)
                .WithMessage("Please Recheck you password");
        }
    }
}
