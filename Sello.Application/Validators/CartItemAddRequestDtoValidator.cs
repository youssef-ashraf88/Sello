using FluentValidation;
using FluentValidation.Validators;
using Sello.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Application.Validators
{
    public class CartItemAddRequestDtoValidator : AbstractValidator<CartItemAddRequestDto>
    {
        public CartItemAddRequestDtoValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty()
                .WithMessage("ProductId is required.");


            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than zero.");
        }
       
    }
}
