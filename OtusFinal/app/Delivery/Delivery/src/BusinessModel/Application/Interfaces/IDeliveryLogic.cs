using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IDeliveryLogic
    {
        Task<int> AddReserveCourier(ReserveCourier reserveCourier, CancellationToken cancel);
        Task<int> DeleteReserveCourier(int reserveId,  CancellationToken cancel);
    }
}
