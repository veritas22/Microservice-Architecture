using Application.Prod.Commands.CreateProduct;
using Application.Prod.Commands.CreateReserve;
using Application.Prod.Commands.DeleteReserve;
using Application.Prod.Queries.GetReserveDetails;
using AutoMapper;
using Domain;
using Grpc.Core;
using Grps;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
namespace Grps.Services
{
    public class WarehouseServise : WarehouseServiseGrps.WarehouseServiseGrpsBase
    {
        private readonly ILogger<WarehouseServise> _logger;
        private IMediator _mediator;
        private readonly IMapper _mapper;

        public WarehouseServise(ILogger<WarehouseServise> logger, IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;
        }

        public override async Task<Reserve> AddReserve(ProductId productId, ServerCallContext context)
        {
            var res = new CreateReserveCommand();
            res.ProductId = productId.ProductId_;
            var result = await _mediator.Send(res);

            return new Reserve { ReserveId = result };
        }

        public override async Task<StatusReply> DeleteReserve(Reserve reserve, ServerCallContext context)
        {
            var res = new DeleteReserveCommand();
            res.ReserveId = reserve.ReserveId;

            var result = await _mediator.Send(res);

            return new StatusReply { Status = result};
        }

        public override async Task<ReserveDto> GetReserve(ProductId productId, ServerCallContext context)
        {
            var res = new GetReserveQuery() { ProductId = productId.ProductId_ };

            var result = await _mediator.Send(res);

            return new ReserveDto() {Id = result.Id,  };
        }
    }
}
