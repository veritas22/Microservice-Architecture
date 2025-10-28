using System;
using FluentValidation;

namespace Application.Prod.Commands.DeleteReserve
{
    public class DeleteReserveCommandValidator : AbstractValidator<DeleteReserveCommand>
    {
        public DeleteReserveCommandValidator()
        {
            RuleFor(createNoteCommand =>
                createNoteCommand.ReserveId).NotEmpty();
        }
    }
}
