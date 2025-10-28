using System;
using MediatR;

namespace Application.Prod.Commands.CreateReserve
{
    public class CreateReserveCommand : IRequest<int>
    {
        public int ProductId { get; set; }
        public bool Status { get; set; }
    }
}
