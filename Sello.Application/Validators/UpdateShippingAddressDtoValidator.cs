using FluentValidation;
using Sello.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Application.Validators
{
    public class UpdateShippingAddressDtoValidator : AbstractValidator<UpdateShippingAddressDto>
    {
        public UpdateShippingAddressDtoValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .When(x => x.FullName != null)
                .WithMessage("Full name cannot be empty.")
                .MaximumLength(100)
                .When(x => x.FullName != null)
                .WithMessage("Full name cannot exceed 100 characters.");

            RuleFor(x => x.AddressLine)
                .NotEmpty()
                .When(x => x.AddressLine != null)
                .WithMessage("Address line cannot be empty.")
                .MaximumLength(200)
                .When(x => x.AddressLine != null)
                .WithMessage("Address line cannot exceed 200 characters.");

            RuleFor(x => x.City)
                .NotEmpty()
                .When(x => x.City != null)
                .WithMessage("City cannot be empty.")
                .MaximumLength(100)
                .When(x => x.City != null)
                .WithMessage("City cannot exceed 100 characters.");

            RuleFor(x => x.State)
                .NotEmpty()
                .When(x => x.State != null)
                .WithMessage("State cannot be empty.")
                .MaximumLength(100)
                .When(x => x.State != null)
                .WithMessage("State cannot exceed 100 characters.");

            RuleFor(x => x.PostalCode)
                .NotEmpty()
                .When(x => x.PostalCode != null)
                .WithMessage("Postal code cannot be empty.")
                .MaximumLength(20)
                .When(x => x.PostalCode != null)
                .WithMessage("Postal code cannot exceed 20 characters.");

            RuleFor(x => x.Country)
                .NotEmpty()
                .When(x => x.Country != null)
                .WithMessage("Country cannot be empty.")
                .MaximumLength(100)
                .When(x => x.Country != null)
                .WithMessage("Country cannot exceed 100 characters.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .When(x => x.PhoneNumber != null)
                .WithMessage("Phone number cannot be empty.")
                .Matches(@"^\+?[1-9]\d{1,14}$")
                .When(x => x.PhoneNumber != null)
                .WithMessage("Phone number must be in E.164 format.");
        }
    }
}
