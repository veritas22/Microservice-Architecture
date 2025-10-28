using Application.Interfaces;
using Domain;
using Interfases;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Prod.Commands.DeleteReserve
{
    public class DeleteReserveCommandHandler
        :IRequestHandler<DeleteReserveCommand, int>
    {
        private readonly IEFStore _iEFStore;

        public DeleteReserveCommandHandler(IEFStore iEFStore) =>
            _iEFStore = iEFStore;

        public async Task<int> Handle(DeleteReserveCommand request,
            CancellationToken cancellationToken)
        {

            var reserve = await _iEFStore.GetReserve(request.ReserveId, cancellationToken);

            var result = await _iEFStore.DeleteReserve(reserve.FirstOrDefault(), cancellationToken);

            return result;
        }
    }
}
