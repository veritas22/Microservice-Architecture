using System;
using FluentValidation;

namespace Application.Prod.Queries.GetProductDetails
{
    public class GetProductDetailsQueryValidator : AbstractValidator<GetProductQuery>
    {
        public GetProductDetailsQueryValidator()
        {
            RuleFor(note => note.Id).NotEmpty();
        }
    }
}
