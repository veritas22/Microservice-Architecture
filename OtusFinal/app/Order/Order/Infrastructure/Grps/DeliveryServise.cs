

using Application.Interfaces;
using Domen;
using Entity;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Threading;
using static Grps.DeliveryServiseGrps;

namespace Grps
{
    public class DeliveryServise : IDelivery
    {
        readonly DeliveryServiseGrpsClient _warehouseServiseGrps;
        public DeliveryServise(DeliveryServiseGrpsClient warehouseServiseGrps)
        {
            _warehouseServiseGrps = warehouseServiseGrps;
        }

        public async Task<int> AddReserveAsync(int order, int userId, CancellationToken cancel)
        {
            try
            {
                var reserveCourier = new ReserveCourier();
                reserveCourier.OrderId = order;
                reserveCourier.UserId = userId;
                reserveCourier.CourierId = 1;
                reserveCourier.TimeSlot = DateTime.Now;
                DateTime utcDateTime = DateTime.SpecifyKind(reserveCourier.TimeSlot, DateTimeKind.Utc);

                var result = await _warehouseServiseGrps.AddReserveCourierAsync(
                     new ReserveRequestDelivery
                     {
                        CourierId  = reserveCourier.CourierId,
                        OrderId = reserveCourier.OrderId,
                        TimeSlot = Timestamp.FromDateTime(utcDateTime),
                        UserId = reserveCourier.UserId
                     }, cancellationToken: cancel);
                return result.ReserveId;

            }
            catch (Exception ex) 
            {
                return -1;
            }

        }


        public async Task<int> DeleteReserveAsync(int reserveId, CancellationToken cancel)
        {
            try
            {
                var result = await _warehouseServiseGrps.DeleteReserveCourierAsync(
                 new ReserveReplyDelivery
                 {
                     ReserveId = reserveId,
                 }, cancellationToken: cancel);
                return result.Status;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }

}
