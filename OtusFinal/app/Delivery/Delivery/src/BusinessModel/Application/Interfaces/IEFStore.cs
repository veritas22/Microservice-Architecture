using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IEFStore
    {
        Task Migrate(CancellationToken cancel);
        Task<ReserveCourier> GetReserve(int Id, CancellationToken cancel);
        Task<int> AddReserve(ReserveCourier reserveCourier, CancellationToken cancel);
        Task<int> DeleteReserve(ReserveCourier reserve, CancellationToken cancel);

    }
}
