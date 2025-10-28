using Entity.Dto;
using Entity.User;

namespace Application.Interfaces
{
    public interface IAuthRepository
    {
        Task<int> AddAccount(Account account);
        Task<Account> GetAccount(string telephone);

        Task UpdateUser(int userId, AccountDto userDto, CancellationToken cancel);
    }
}
