using System;
using MediatR;

namespace Application.Prod.Queries.GetReserveDetails
{
    public class GetReserveQuery : IRequest<ReserveDetailsVm>
    {
        public int ProductId { get; set; }
    }
}
