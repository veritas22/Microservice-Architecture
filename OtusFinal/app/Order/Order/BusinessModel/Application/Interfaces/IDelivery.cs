using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IDelivery
    {
        Task<int> AddReserveAsync(int order, int userId, CancellationToken cancel);
        Task<int> DeleteReserveAsync(int reserveId, CancellationToken cancel);
    }
}
