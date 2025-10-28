using Application.Interfaces;
using Entity;
using Grpc.Core;
using GrpcMailService.Services;
using Grps;
using Microsoft.Extensions.Logging;
namespace Grps.Services
{
    public class AccountServise : AccountServiseGrps.AccountServiseGrpsBase
    {
        private readonly ILogger<AccountServise> _logger;
        private readonly IAccountLogic _accountLogic;
        public AccountServise(ILogger<AccountServise> logger, IAccountLogic accountLogic)
        {
            _logger = logger;
            _accountLogic = accountLogic;
        }

        public override async Task<AccountReply> AddAccount(AccountRequest request, ServerCallContext context)
        {
            var result = await _accountLogic.AddAccount(request.UserId);

            var accountReply = new AccountReply();
            accountReply.AccountId = result;
            return accountReply;
        }
    }
}
