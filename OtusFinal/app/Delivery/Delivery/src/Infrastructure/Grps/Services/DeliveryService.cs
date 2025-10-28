using Application.Interfaces;
using Entity;
using Grpc.Core;
using Grps;
using Microsoft.Extensions.Logging;
using System;


namespace GrpcService.Services
{
    public class DeliveryService : DeliveryServiseGrps.DeliveryServiseGrpsBase
    {
        private readonly ILogger<DeliveryService> _logger;
        private readonly IDeliveryLogic _deliveryLogic;
        public DeliveryService(ILogger<DeliveryService> logger, IDeliveryLogic deliveryLogic)
        {
            _logger = logger;
            _deliveryLogic = deliveryLogic;

        }

        public override async Task<ReserveReplyDelivery> AddReserveCourier(ReserveRequestDelivery reserveRequest, ServerCallContext context)
        {
            var date = DateTime.SpecifyKind(reserveRequest.TimeSlot.ToDateTime(), DateTimeKind.Unspecified);

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken cancel = cancelTokenSource.Token;
            var res = new ReserveCourier();
            res.OrderId = reserveRequest.OrderId;
            res.CourierId = reserveRequest.CourierId;
            res.UserId = reserveRequest.UserId;
            res.TimeSlot = date;
            var result = await _deliveryLogic.AddReserveCourier(res, cancel);
            return new ReserveReplyDelivery
            {
                ReserveId = result
            };
        }

        public override async Task<StatusReplyDelivery> DeleteReserveCourier(ReserveReplyDelivery reserveReply, ServerCallContext context)
        {
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken cancel = cancelTokenSource.Token;
            var result = await _deliveryLogic.DeleteReserveCourier(reserveReply.ReserveId, cancel);
            return new StatusReplyDelivery
            {
                Status = result
            };
        }
    }
}