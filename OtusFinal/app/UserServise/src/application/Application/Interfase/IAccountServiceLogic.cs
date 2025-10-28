
using Entity.Dto;
using Entity.User;

namespace Application.Interfaces
{
    public interface IAccountServiceLogic
    {
        public Task<int> Register(string userName, string password, string telephone);
        public Task<string> GetAccountPass(string password, string telephone);
        public Task<Account> GetAccount(string telephone);

        Task UpdateUser(int userId, AccountDto accountDto, CancellationToken cancel);

    }
}
