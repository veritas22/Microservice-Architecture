using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICache
    {
        Task<string> SetRedis();
        Task<string> GetRedis();
    }
}
