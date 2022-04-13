namespace RyGamingProvider.Interface
{
    public interface IPlayerRepository
    {
        double WalletBalance { get; set; }

        double Bet(double amount);

        bool Login(string name, string password, out string message, out string token);

        double TopUp(double amount);

        double Withdraw(double amount);
    }
}
