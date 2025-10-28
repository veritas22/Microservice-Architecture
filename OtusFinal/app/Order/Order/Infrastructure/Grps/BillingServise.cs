

using Application.Interfaces;
using static Grps.BillingServiseGrps;
using static System.Net.Mime.MediaTypeNames;

namespace Grps
{
    public class BillingServise: IBillling
    {
        readonly BillingServiseGrpsClient _billingServiseGrps;
        public BillingServise(BillingServiseGrpsClient billingServiseGrps)
        {
            _billingServiseGrps = billingServiseGrps;
        }

        public async Task<float> WithdrawMoneyAsync(int userId, float amount)
        {
            try
            {
                var tt = await _billingServiseGrps.WithdrawMoneyAsync(
                     new BillingRequest
                     {
                         UserId = userId,
                         Amount = amount,
                     });
                return tt.Amount;
            }
            catch 
            {
                return -1;
            }
        }

        public async Task<float> DepositMoneyAsync(int userId, float amount)
        {
            try
            {
                var tt = await _billingServiseGrps.DepositMoneyAsync(
                     new BillingRequest
                     {
                         UserId = userId,
                         Amount = amount,
                     });
                return tt.Amount;
            }
            catch
            {
                return -1;
            }
        }
    }

}
