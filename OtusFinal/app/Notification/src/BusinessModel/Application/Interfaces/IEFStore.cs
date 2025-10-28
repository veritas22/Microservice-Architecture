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
        Task<IEnumerable<Mail>> GetMail(int userId, CancellationToken cancel);
        Task AddMail(Mail mail, CancellationToken cancel);
    }
}
