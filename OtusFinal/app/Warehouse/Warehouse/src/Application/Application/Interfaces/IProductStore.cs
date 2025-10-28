using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProductStore
    {
        Task<IEnumerable<Product>> GetProduct(int userId, CancellationToken cancel);

        Task<int> AddProduct(Product product, CancellationToken cancel);
    }
}
