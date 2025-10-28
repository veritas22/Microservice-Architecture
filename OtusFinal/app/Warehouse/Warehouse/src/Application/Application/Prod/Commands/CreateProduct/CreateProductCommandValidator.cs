using System;
using FluentValidation;

namespace Application.Prod.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(createNoteCommand =>
                createNoteCommand.Price).NotEmpty();

        }
    }
}
