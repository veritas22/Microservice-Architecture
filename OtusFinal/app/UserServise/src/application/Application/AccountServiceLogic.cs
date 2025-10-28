using Application.Interfaces;
using Entity.Dto;
using Entity.User;
using Grps.Services;
using Microsoft.AspNetCore.Identity;

namespace Application
{
    public class AccountServiceLogic : IAccountServiceLogic
    {
        private readonly IAuthRepository _authRepository;
        private readonly Jwt _jwt;
        IAccountServise _accountServise;
        public AccountServiceLogic(IAuthRepository authRepository, Jwt jwt, IAccountServise accountServise)
        {
            _authRepository = authRepository;
            _jwt = jwt;
            _accountServise = accountServise;
        }

        public async Task<int> Register(string userName, string password, string telephone)
        {
            var account = new Account() {  UserName = userName, Telephone = telephone };
            var telepAc =  await _authRepository.GetAccount(telephone);
            if (telepAc != null)
            {
                return telepAc.Id;
            }
            var pasHash = new PasswordHasher<Account>().HashPassword(account, password);
            account.PasswordHash = pasHash;
            var id = await _authRepository.AddAccount(account);
            if (id >= 0)
            {
                var res = await _accountServise.AddAccount(id);
                return id;
            }
            return -1;
        }

        public async Task<string> GetAccountPass(string password, string telephone)
        {
            var account = await _authRepository.GetAccount(telephone);
            var verify = new PasswordHasher<Account>().VerifyHashedPassword(account, account.PasswordHash, password);
            if (verify == PasswordVerificationResult.Success)
            {
                return _jwt.GenerateToken(account);
            }
            else 
            {
                throw new Exception("не по плану");
            
            }
        }

        public async Task<Account> GetAccount(string telephone)
        {
            return await _authRepository.GetAccount(telephone);
        }
        public async Task UpdateUser(int userId, AccountDto accountDto, CancellationToken cancel)
        {
             await _authRepository.UpdateUser(userId, accountDto, cancel);
        }
    }
}
