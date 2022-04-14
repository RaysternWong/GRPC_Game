using Grpc.Net.Client;
using RyGamingWallet;

namespace RyGamingWalletClientLib
{
    public class FundTransferResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public double Balance { get; set; }
    }

    public class TopUpResponse : FundTransferResponse
    { }

    public class WithdrawResponse : FundTransferResponse
    { }

    public class CreateResponse : FundTransferResponse
    { }

    public class CheckResponse : FundTransferResponse
    {
    }

    public class WalletClient
    {
        private GrpcChannel _channel;
        private const string Address = "localhost:5001";

        public WalletClient(string address)
        {
            _channel = GrpcChannel.ForAddress($"https://{address}");
        }

        public CreateResponse CreateWallet(string token, double initialBalance)
        {
            var client = new RyGamerWallet.RyGamerWalletClient(_channel);

            var request = new CreateWalletRequest()
            {
                Token = token,
                FundAmount = initialBalance
            };

            var response = client.Create(request);

            return new CreateResponse
            {
                Success = response.Success,
                Message = response.Message,
                Balance = response.Balance
            };
        }

        public TopUpResponse TopUp(string token, double balance)
        {
            var client = new RyGamerWallet.RyGamerWalletClient(_channel);

            var request = new WalletTopUpRequest()
            {
                Token = token,
                Amount = balance
            };

            var response = client.TopUp(request);

            return new TopUpResponse
            {
                Success = response.Success,
                Message = response.Message,
                Balance = response.Balance
            };
        }

        public WithdrawResponse Withdraw(string token, double balance)
        {
            var client = new RyGamerWallet.RyGamerWalletClient(_channel);

            var request = new WalletWithdrawRequest()
            {
                Token = token,
                Amount = balance
            };

            var response = client.Withdraw(request);

            return new WithdrawResponse
            {
                Success = response.Success,
                Message = response.Message,
                Balance = response.Balance
            };
        }

        public CheckResponse Check(string token)
        {
            var client = new RyGamerWallet.RyGamerWalletClient(_channel);

            var request = new WalletCheckRequest()
            {
                Token = token,
            };

            var response = client.Check(request);

            return new CheckResponse
            {
                Success = response.Success,
                Message = response.Message,
                Balance = response.Balance
            };
        }
    }
}
