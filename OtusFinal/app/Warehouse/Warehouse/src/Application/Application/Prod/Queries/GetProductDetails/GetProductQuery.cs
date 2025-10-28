using System;
using MediatR;

namespace Application.Prod.Queries.GetProductDetails
{
    public class GetProductQuery : IRequest<ProductDetailsVm>
    {
        public int Id { get; set; }
    }
}
