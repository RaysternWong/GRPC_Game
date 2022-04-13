using Microsoft.Extensions.Logging;
using System;

namespace RyGamingProvider
{
    public class PlayerRepository
    {
        private readonly ILogger<PlayerRepository> _logger;
        private int _availableTickets = 5;

        private const string Name = "Win10";
        private const string Password = "123";

        public double WalletBalance { get; set; } = 100;

        public PlayerRepository(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<PlayerRepository>();
        }

        public bool Login(string name, string password, out string message, out string token)
        {
            bool success = false;
            token = string.Empty;

            if (name == Name)
            {
                if (password == Password)
                {
                    message = "Login Success";
                    token = TokenCreater.GenerateJwtToken(name);
                    success = true;
                }
                else
                {
                    message = "Password is wrong";
                }
            }
            else
            {
                message = "User is not exist";
            }

            return success;
        }

        public double TopUp(double amount)
        {
            WalletBalance += amount;

            return WalletBalance;
        }

        public double Withdraw(double amount)
        {
            if (amount > WalletBalance)
            {
                throw new Exception("Withdraw Amount is more than wallet balance");
            }

            WalletBalance -= amount;

            return WalletBalance;
        }

        public int GetAvailableTickets()
        {
            return _availableTickets;
        }

        public double Bet(double amount)
        {
            if (amount > WalletBalance)
            {
                throw new Exception("Bet Amount is more than wallet balance");
            }

            Random random = new Random();
            double lossMax = -amount;
            double winMax = amount;
            double result = (random.NextDouble() * (winMax - lossMax) + lossMax);

            WalletBalance += result;

            return result;
        }

        public bool BuyTickets(string user, int count)
        {
            var updatedCount = _availableTickets - count;

            // Negative ticket count means there weren't enough available tickets
            if (updatedCount < 0)
            {
                _logger.LogWarning("{User} failed to purchase tickets. Not enough available tickets.", user);
                return false;
            }

            _availableTickets = updatedCount;

            _logger.LogInformation("{User} successfully purchased tickets.", user);
            return true;
        }
    }
}
