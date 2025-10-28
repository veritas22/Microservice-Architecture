using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IWarehouse
    {
        Task<int> AddReserveAsync(int productId);
        Task<int> DeleteReserveAsync(int reserveId);
    }
}
