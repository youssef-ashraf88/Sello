using FluentValidation;
using Sello.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Application.Validators
{
    public sealed class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(r => r.Username)
                .NotEmpty()
                .WithMessage("user name is required");

            RuleFor(r => r.Email)
                .NotEmpty()
                .WithMessage("Email address is required")
                .EmailAddress()
                .WithMessage("Email should be in a proper format");

            RuleFor(r => r.Password)
                .NotEmpty()
                .WithMessage("Password is required");

            RuleFor(r => r.ConfirmPassword)
                .NotEmpty()
                .WithMessage("Confirm password is required")
                .Equal(r => r.Password)
                .WithMessage("password and confirm password should match");

            RuleFor(r => r.PhoneNumber)
                .NotEmpty()
                .WithMessage("Phone number is required")
                .Matches("^[0-9]*$")
                .WithMessage("Phone number should contain digits only");
        }
    }
}
