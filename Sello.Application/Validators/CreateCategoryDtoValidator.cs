using FluentValidation;
using Sello.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Application.Validators
{
    public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryDtoValidator()
        {
            RuleFor(cc => cc.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .MaximumLength(50)
                .WithMessage("Name can't be more than 50 characters");

            RuleFor(cc => cc.Description)
                .NotEmpty()
                .WithMessage("Name is required")
                .MaximumLength(700)
                .WithMessage("Name can't be more than 700 characters");
        }
    }
}
