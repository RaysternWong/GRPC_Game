using RyGamingProvider.Interface;
using System;

namespace RyGamingProvider
{
    public class PlayerRepository : IPlayerRepository
    {
        private const string Name = "Win10";
        private const string Password = "123";

        public double WalletBalance { get; set; } = 100;

        public PlayerRepository()
        {
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

        public double Bet(double amount)
        {
            if (amount > WalletBalance)
            {
                throw new Exception($"Bet Amount : {amount} is more than wallet balance");
            }

            Random random = new Random();
            double lossMax = -amount;
            double winMax = amount;
            double result = (random.NextDouble() * (winMax - lossMax) + lossMax);

            result = Math.Round(result, 2);
            WalletBalance += result;

            WalletBalance = Math.Round(WalletBalance, 2);

            return result;
        }
    }
}
