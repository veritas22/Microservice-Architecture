using Application.Interfaces;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BusinessLogic
{
    public class BillingLogic: IBillingLogic
    {
        readonly IEFStore _iEFStore;
        public BillingLogic(IEFStore iEFStore)
        {
            _iEFStore = iEFStore;
        }

        public async Task<float> WithdrawMoney(int userId,float amount)
        {
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken cancel = cancelTokenSource.Token;
            var account = new Account();
            account.UserId = userId;
            var result =await _iEFStore.GetAccount(userId, cancel);
            if (result != null && result.Amount >= amount)
            {
                var amountWithdraw = result.Amount - amount;
                return await _iEFStore.UpdateAccount(userId, amountWithdraw, cancel);
            }
            else
            {
                return -1;
            }
        }

        public async Task<float> DepositMoney(int userId, float amount, CancellationToken cancel)
        {

            var result = await _iEFStore.GetAccount(userId, cancel);
            if (result != null)
            {
                var amountWithdraw = result.Amount + amount;
                return await _iEFStore.UpdateAccount(userId, amountWithdraw, cancel);
            }
            else
            {
                return -1;
            }
        }
    }
}
