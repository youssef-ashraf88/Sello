using FluentValidation;
using Sello.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Application.Validators
{
    public sealed class UpdateCategoryDtoValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryDtoValidator()
        {
            RuleFor(uc => uc.Name)
                .MaximumLength(50)
                .WithMessage("Name can't be more than 50 character");

            RuleFor(uc => uc.Description)
                .MaximumLength(700)
                .WithMessage("Description can't be more than 700 character");
        }
    }
}
