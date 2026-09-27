using FluentValidation;
using Sello.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Application.Validators
{
    public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductDtoValidator()
        {
            RuleFor(cp => cp.CategoryId)
                .NotEmpty()
                .WithMessage("Category id is required");

            RuleFor(cp => cp.Name)
                .MaximumLength(50)
                .WithMessage("Name can't be more than 50 characters");

            RuleFor(cp => cp.Description)
                .MaximumLength(500)
                .WithMessage("Description can't be more than 500 characters");

            RuleFor(cp => cp.StockQuantity)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Stock quantity can't be negative");

            RuleFor(cp => cp.Price)
                .GreaterThan(0)
                .WithMessage("Price must be greater than 0");
        }
    }
}
