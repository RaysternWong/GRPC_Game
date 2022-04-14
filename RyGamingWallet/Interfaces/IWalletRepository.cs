namespace RyGamingWallet
{
    public interface IWalletRepository
    {
        double CreateWallet(string token, double fundAmount);

        double TopUp(string token, double amount);

        double Withdraw(string token, double amount);
    }
}
