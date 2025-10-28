using Application.Interfaces;
using Application.Interfaces.Broker;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BusinessLogic
{
    public class AccountLogic: IAccountLogic
    {
        readonly IEFStore _iEFStore;
        public AccountLogic(IEFStore iEFStore) 
        {
            _iEFStore = iEFStore;
        }

        public async Task<int> AddAccount(int accountId)
        {
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken cancel = cancelTokenSource.Token;
            var account = new Account();
            account.UserId = accountId;
            return await _iEFStore.AddAccount(account, cancel);

        }

    }
}
