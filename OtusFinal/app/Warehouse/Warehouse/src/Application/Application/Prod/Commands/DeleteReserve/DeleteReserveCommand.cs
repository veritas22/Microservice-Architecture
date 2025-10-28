using System;
using MediatR;

namespace Application.Prod.Commands.DeleteReserve
{
    public class DeleteReserveCommand : IRequest<int>
    {
        public int ReserveId { get; set; }
    }
}
