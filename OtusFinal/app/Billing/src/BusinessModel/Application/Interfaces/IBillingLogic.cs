using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBillingLogic
    {
        Task<float> WithdrawMoney(int userId, float amount);
        Task<float> DepositMoney(int userId, float amount, CancellationToken cancel);
    }
}
