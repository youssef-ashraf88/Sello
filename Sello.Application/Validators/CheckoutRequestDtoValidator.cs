using FluentValidation;
using Sello.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Application.Validators
{
    public class CheckoutRequestDtoValidator : AbstractValidator<CheckoutRequestDto>
    {
        public CheckoutRequestDtoValidator()
        {
            RuleFor(c => c.ShippingAddressId)
                .NotEmpty()
                .WithMessage("Address id is required");
        }
    }
}
