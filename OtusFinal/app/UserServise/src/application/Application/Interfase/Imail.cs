using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfase
{
    public interface Imail
    {
        Task<int> Send();
    }
}
