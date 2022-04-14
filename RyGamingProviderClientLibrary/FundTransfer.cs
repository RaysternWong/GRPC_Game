using RyGamingWalletClientLib;

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

    public class CreateResponse : FundTransferResponse
    { }

    public class FundTransfer
    {
        private string _token;
        private WalletClient _walletClient;

        public FundTransfer(string token)
        {
            _token = token;
            _walletClient = new WalletClient("localhost:5001");
        }

        public TopUpResponse TopUp(double amount)
        {
            var response = _walletClient.TopUp(_token, amount);

            return new TopUpResponse
            {
                Success = response.Success,
                Message = response.Message,
                BalanceAfter = response.Balance
            };
        }

        public CreateResponse Create(double amount)
        {
            var response = _walletClient.CreateWallet(_token, amount);

            return new CreateResponse
            {
                Success = response.Success,
                Message = response.Message,
                BalanceAfter = response.Balance
            };
        }

        public WithdrawResponse Withdraw(double amount)
        {
            var response = _walletClient.Withdraw(_token, amount);

            return new WithdrawResponse
            {
                Success = response.Success,
                Message = response.Message,
                BalanceAfter = response.Balance
            };
        }
    }
}
