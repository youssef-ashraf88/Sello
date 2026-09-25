using FluentValidation;
using Sello.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Application.Validators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(l => l.Email)
                .NotEmpty()
                .WithMessage("Email is reqired")
                .EmailAddress()
                .WithMessage("Email should be in a proper format");

            RuleFor(l => l.Password)
                .NotEmpty()
                .WithMessage("Password is required");
        }
    }
}
