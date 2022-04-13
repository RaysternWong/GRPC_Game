using RyGamingProviderClientLibrary;
using System;

namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var authentication = new Authentication();
            var transfer = new FundTransfer();
            var gameBet = new GameBet();

            var response = authentication.Login("Win10", "123");

            var topUpRes = transfer.TopUp(100);

            var WithdrawRes = transfer.Withdraw(100);

            var betResponse = gameBet.SingleBet(10);

            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
