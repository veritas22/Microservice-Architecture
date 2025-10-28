using Application.Interfaces;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BusinessLogic
{
    public class DeliveryLogic: IDeliveryLogic
    {
        readonly IEFStore _iEFStore;
        public DeliveryLogic(IEFStore iEFStore)
        {
            _iEFStore = iEFStore;
        }

        public async Task<int> AddReserveCourier(ReserveCourier reserveCourier, CancellationToken cancel)
        {
            var result = await _iEFStore.AddReserve(reserveCourier, cancel);
            return result;
        }
        public async Task<int> DeleteReserveCourier(int reserveId, CancellationToken cancel)
        {
            var reserve = await _iEFStore.GetReserve(reserveId, cancel);

            var result = await _iEFStore.DeleteReserve(reserve, cancel);
            return result;
        }
    }
}
