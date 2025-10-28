using System;
using FluentValidation;

namespace Application.Prod.Queries.GetReserveDetails
{
    public class GetReserveQueryValidator : AbstractValidator<GetReserveQuery>
    {
        public GetReserveQueryValidator()
        {
            RuleFor(note => note.ProductId).NotEmpty();
        }
    }
}
