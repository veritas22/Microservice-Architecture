using static Grps.AccountServiseGrps;

using Grps;
using Microsoft.Extensions.Logging;
namespace Grps.Services
{

    public class AccountServise : IAccountServise
    {
        readonly AccountServiseGrpsClient _accountServise;
        public AccountServise(AccountServiseGrpsClient billingServiseGrps)
        {
            _accountServise = billingServiseGrps;
        }

        public async Task<int> AddAccount(int userId)
        {
            var account= await _accountServise.AddAccountAsync(new AccountRequest() {UserId = userId });

            return account.AccountId;
        }
    }
}
