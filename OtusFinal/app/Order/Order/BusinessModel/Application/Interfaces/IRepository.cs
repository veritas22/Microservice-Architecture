using Domen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<int> AddAsync(TEntity order, CancellationToken cancel);
        Task<int> DeleteAsync(TEntity order, CancellationToken cancel);

        Task<TEntity> GetAsync(int userId, CancellationToken cancel);
    }
}
