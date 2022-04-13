using Microsoft.Extensions.Logging;
using System;

namespace RyGamingProvider
{
    public class PlayerRepository
    {
        private readonly ILogger<PlayerRepository> _logger;
        private int _availableTickets = 5;
        private double _walletBalance = 0;

        private const string Name = "Admin";
        private const string Password = "123";

        public PlayerRepository(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<PlayerRepository>();
        }

        public bool Login(string name, string password)
        {
            if (name == Name && password == Password)
            {
                return true;
            }

            return false;
        }

        public double TopUp(double amount)
        {
            _walletBalance += amount;

            return _walletBalance;
        }

        public double Withdraw(double amount)
        {
            if (amount > _walletBalance)
            {
                throw new Exception("Withdraw Amount is more than wallet balance");
            }

            _walletBalance -= amount;

            return _walletBalance;
        }

        public int GetAvailableTickets()
        {
            return _availableTickets;
        }

        public double Bet(double amount)
        {
            if (amount > _walletBalance)
            {
                throw new Exception("Bet Amount is more than wallet balance");
            }

            Random random = new Random();
            double lossMax = -amount;
            double winMax = amount;
            double result = (random.NextDouble() * (winMax - lossMax) + lossMax);

            _walletBalance += result;

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
