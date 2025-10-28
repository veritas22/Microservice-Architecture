using Application.Interfaces;
using Grpc.Core;
using Grps;
using Microsoft.Extensions.Logging;

namespace GrpcMailService.Services;

public class BillingServise : BillingServiseGrps.BillingServiseGrpsBase
{
    private readonly ILogger<BillingServise> _logger;
    private readonly IBillingLogic _billingLogic;
    public BillingServise(ILogger<BillingServise> logger, IBillingLogic billingLogic)
    {
        _logger = logger;
        _billingLogic = billingLogic;

    }

    public override async Task<BillingReply> WithdrawMoney(BillingRequest request, ServerCallContext context)
    {
        var result = await _billingLogic.WithdrawMoney(request.UserId, request.Amount);
        return new BillingReply
        {
            Amount = result
        };
    }

    public override async Task<BillingReply> DepositMoney(BillingRequest request, ServerCallContext context)
    {
        CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        CancellationToken cancel = cancelTokenSource.Token;
        var result = await _billingLogic.DepositMoney(request.UserId, request.Amount, cancel);
        return new BillingReply
        {
            Amount = result
        };
    }
}
