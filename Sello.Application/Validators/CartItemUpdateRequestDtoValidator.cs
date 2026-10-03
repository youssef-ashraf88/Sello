using FluentValidation;
using Sello.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Application.Validators
{
    public class CartItemUpdateRequestDtoValidator : AbstractValidator<CartItemUpdateRequestDto>
    {
        public CartItemUpdateRequestDtoValidator()
        {
            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than 0.");
        }
    }
}
