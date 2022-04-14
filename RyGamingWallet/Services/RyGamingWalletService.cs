using Grpc.Core;
using System;
using System.Threading.Tasks;

namespace RyGamingWallet
{
    public class RyGamingWalletService : RyGamerWallet.RyGamerWalletBase
    {
        private readonly IWalletRepository _walletRepository;

        public RyGamingWalletService(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public override Task<CreateWalletResponse> Create(CreateWalletRequest request, ServerCallContext context)
        {
            bool success = true;
            string message = string.Empty;
            double initialBalance = 0;

            try
            {
                initialBalance = _walletRepository.CreateWallet(request.Token, request.FundAmount);
            }
            catch (Exception ex)
            {
                message = ex.Message;
                success = false;
            }

            return Task.FromResult(new CreateWalletResponse()
            {
                Success = success,
                Message = message,
                Balance = initialBalance
            });
        }

        public override Task<WalletTopUpResponse> TopUp(WalletTopUpRequest request, ServerCallContext context)
        {
            bool success = true;
            string message = string.Empty;
            double balance = 0;

            try
            {
                balance = _walletRepository.TopUp(request.Token, request.Amount);
            }
            catch (Exception ex)
            {
                message = ex.Message;
                success = false;
            }

            return Task.FromResult(new WalletTopUpResponse()
            {
                Success = success,
                Message = message,
                Balance = balance
            });
        }

        public override Task<WalletWithdrawResponse> Withdraw(WalletWithdrawRequest request, ServerCallContext context)
        {
            bool success = true;
            string message = string.Empty;
            double balance = 0;

            try
            {
                balance = _walletRepository.Withdraw(request.Token, request.Amount);
            }
            catch (Exception ex)
            {
                message = ex.Message;
                success = false;
            }

            return Task.FromResult(new WalletWithdrawResponse()
            {
                Success = success,
                Message = message,
                Balance = balance
            });
        }
    }
}
