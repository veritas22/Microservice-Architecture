using Application.Common.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain;
using Interfases;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Prod.Queries.GetReserveDetails
{
    public class GetReserveQueryHandler
        : IRequestHandler<GetReserveQuery, ReserveDetailsVm>
    {
        private readonly IEFStore _iEFStore;
        private readonly IMapper _mapper;

        public GetReserveQueryHandler(IEFStore iEFStore, IMapper mapper) 
        {
            _iEFStore = iEFStore;
            _mapper = mapper;
        }

        public async Task<ReserveDetailsVm> Handle(GetReserveQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await _iEFStore.GetReserve(request.ProductId, cancellationToken);

            return _mapper.Map<ReserveDetailsVm>(entity.FirstOrDefault());
        }
    }
}
