using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using RyGaming;
using System;
using System.Threading.Tasks;

namespace RyGamingProvider.Services
{
    public class RyGamingService : RyGaming.RyGamer.RyGamerBase
    {
        private readonly PlayerRepository _playerRepository;

        public RyGamingService(PlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public override Task<LoginResponse> Login(LoginRequest request, ServerCallContext context)
        {
            string message = string.Empty;
            string token = string.Empty;
            bool isSuccess = _playerRepository.Login(request.Name, request.Password, out message, out token);

            return Task.FromResult(new LoginResponse()
            {
                Success = isSuccess,
                Message = message,
                Token = token
            });
        }

        [Authorize]
        public override Task<WalletTopUpResponse> TopUp(WalletTopUpRequest request, ServerCallContext context)
        {
            bool isSuccess = true;
            double balanceAfter = _playerRepository.WalletBalance;
            string message = $"Successful TopUp :{request.TopUpAmount}";

            try
            {
                balanceAfter = _playerRepository.TopUp(request.TopUpAmount);
            }
            catch (Exception ex)
            {
                isSuccess = false;
                message = ex.Message;
            }

            return Task.FromResult(new WalletTopUpResponse()
            {
                Success = isSuccess,
                Message = message,
                BalanceAfter = balanceAfter
            });
        }

        [Authorize]
        public override Task<WalletWithdrawResponse> Withdraw(WalletWithdrawRequest request, ServerCallContext context)
        {
            bool isSuccess = true;
            double balanceAfter = _playerRepository.WalletBalance;
            string message = $"Successful Withdraw :{request.WithdrawAmount}";

            try
            {
                balanceAfter = _playerRepository.Withdraw(request.WithdrawAmount);
            }
            catch (Exception ex)
            {
                isSuccess = false;
                message = ex.Message;
            }

            return Task.FromResult(new WalletWithdrawResponse()
            {
                Success = isSuccess,
                Message = message,
                BalanceAfter = balanceAfter
            });
        }

        [Authorize]
        public override Task<BetResponse> Bet(BetRequest request, ServerCallContext context)
        {
            bool isSuccess = true;
            double winLossAmount = _playerRepository.WalletBalance;
            string message = $"Successful Withdraw - {request.BetAmount}";

            try
            {
                winLossAmount = _playerRepository.Bet(request.BetAmount);
            }
            catch (Exception ex)
            {
                isSuccess = false;
                message = ex.Message;
            }

            return Task.FromResult(new BetResponse()
            {
                Success = isSuccess,
                Message = message,
                BalanceAfter = _playerRepository.WalletBalance,
                WinLossAmount = winLossAmount
            });
        }
    }
}
