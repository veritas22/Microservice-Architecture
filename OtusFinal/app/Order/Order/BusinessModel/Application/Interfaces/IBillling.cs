using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBillling
    {
        Task<float> WithdrawMoneyAsync(int userId, float amount);
        Task<float> DepositMoneyAsync(int userId, float amount);
    }
}
