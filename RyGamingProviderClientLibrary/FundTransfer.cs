using RyGaming;
using System.Threading.Tasks;

namespace RyGamingProviderClientLibrary
{
    public class FundTransferResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public double BalanceAfter { get; set; }
    }

    public class TopUpResponse : FundTransferResponse
    { }

    public class WithdrawResponse : FundTransferResponse
    { }

    public class FundTransfer
    {
        public FundTransfer(string token)
        {
            Connection._token = token;
        }

        public TopUpResponse TopUp(double amount)
        {
            return TopUpAsync(amount).Result;
        }

        public async Task<TopUpResponse> TopUpAsync(double amount)
        {
            var client = new RyGamer.RyGamerClient(Connection.Channel);

            var request = new WalletTopUpRequest()
            {
                TopUpAmount = amount
            };

            var response = await client.TopUpAsync(request);

            return new TopUpResponse()
            {
                Success = response.Success,
                Message = response.Message,
                BalanceAfter = response.BalanceAfter
            };
        }

        public WithdrawResponse Withdraw(double amount)
        {
            return WithdrawAsync(amount).Result;
        }

        public async Task<WithdrawResponse> WithdrawAsync(double amount)
        {
            var client = new RyGamer.RyGamerClient(Connection.Channel);

            var request = new WalletWithdrawRequest()
            {
                WithdrawAmount = amount
            };

            var response = await client.WithdrawAsync(request);

            return new WithdrawResponse()
            {
                Success = response.Success,
                Message = response.Message,
                BalanceAfter = response.BalanceAfter
            };
        }
    }
}
