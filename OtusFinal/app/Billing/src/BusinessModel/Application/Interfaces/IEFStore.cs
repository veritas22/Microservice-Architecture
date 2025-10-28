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
        Task<Account> GetAccount(int userId, CancellationToken cancel);
        Task<int> AddAccount(Account mail, CancellationToken cancel);
        Task<float> UpdateAccount(int userId, float amount, CancellationToken cancel);

    }
}
