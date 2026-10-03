using FluentValidation;
using Sello.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Application.Validators
{
    public class UpdateOrderStatusRequestDtoValidator : AbstractValidator<UpdateOrderStatusRequestDto>
    {
        public UpdateOrderStatusRequestDtoValidator()
        {
            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage("Invalid order status.");
        }
    }
}
