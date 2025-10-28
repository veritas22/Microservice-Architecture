using Application.Interfaces;
using Domain;
using Interfases;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Prod.Commands.CreateReserve
{
    public class CreateReserveCommandHandler
        : IRequestHandler<CreateReserveCommand, int>
    {
        private readonly IEFStore _iEFStore;

        public CreateReserveCommandHandler(IProductStore productStore, IEFStore iEFStore)
        {
            _iEFStore = iEFStore;
        }


        public async Task<int> Handle(CreateReserveCommand request,
            CancellationToken cancellationToken)
        {
            var reserve = new Reserve
            {
                ProductId = request.ProductId,
                Status = 1,
            };

            var result = await _iEFStore.AddReserve(reserve, cancellationToken);

            return result;
        }
    }
}
