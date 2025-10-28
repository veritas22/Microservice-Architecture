using System;
using FluentValidation;

namespace Application.Prod.Commands.CreateReserve
{
    public class CreateReserveCommandValidator : AbstractValidator<CreateReserveCommand>
    {
        public CreateReserveCommandValidator()
        {
            RuleFor(createNoteCommand =>
                createNoteCommand.ProductId).NotEmpty();
        }
    }
}
