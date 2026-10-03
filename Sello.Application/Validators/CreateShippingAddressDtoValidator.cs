using FluentValidation;
using Sello.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Application.Validators
{
    public class CreateShippingAddressDtoValidator : AbstractValidator<CreateShippingAddressDto>
    {
        public CreateShippingAddressDtoValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .WithMessage("Full name is required.")
                .MaximumLength(100)
                .WithMessage("Full name cannot exceed 100 characters.");

            RuleFor(x => x.AddressLine)
                .NotEmpty()
                .WithMessage("Address line is required.")
                .MaximumLength(200)
                .WithMessage("Address line cannot exceed 200 characters.");
            
            RuleFor(x => x.City)
                .NotEmpty()
                .WithMessage("City is required.")
                .MaximumLength(50)
                .WithMessage("City cannot exceed 50 characters.");
            
            RuleFor(x => x.State)
                .NotEmpty()
                .WithMessage("State is required.")
                .MaximumLength(50)
                .WithMessage("State cannot exceed 50 characters.");
            
            RuleFor(x => x.PostalCode)
                .NotEmpty()
                .WithMessage("Postal code is required.")
                .Matches(@"^\d{5}(-\d{4})?$")
                .WithMessage("Postal code must be in the format '12345' or '12345-6789'.");
            
            RuleFor(x => x.Country)
                .NotEmpty()
                .WithMessage("Country is required.")
                .MaximumLength(50)
                .WithMessage("Country cannot exceed 50 characters.");
            
            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage("Phone number is required.")
                .Matches(@"^\+?[0-9]\d{1,14}$")
                .WithMessage("Phone number must be in E.164 format.");
        }
    }
}
