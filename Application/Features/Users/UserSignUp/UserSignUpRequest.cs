﻿using Application.Common;
using MediatR;

namespace Application.Features.Users.UserSignUp
{
    public sealed record UserSignUpRequest : IRequest<CommonResponse>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword {  get; set; } = string.Empty;
    }
}
