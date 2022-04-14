using System;
using System.Collections.Generic;
using System.Linq;

namespace RyGamingWallet
{
    public class Wallet
    {
        public string Owner { get; set; }

        public double Balance { get; set; }

        public Wallet(string owner, double balance)
        {
            Owner = owner;
            Balance = balance;
        }
    }

    public class WalletRepository : IWalletRepository
    {
        private Dictionary<string, Wallet> _wallets;

        public WalletRepository()
        {
            _wallets = new Dictionary<string, Wallet>();
        }

        public double CreateWallet(string token, double fundAmount)
        {
            if (_wallets.Keys.Contains(token))
            {
                throw new Exception("The wallet for same account has been created");
            }

            _wallets.Add(token, new Wallet(token, fundAmount));

            return fundAmount;
        }

        public double TopUp(string token, double amount)
        {
            if (_wallets.Keys.Contains(token) == false)
            {
                throw new Exception("The wallet account does not exist");
            }

            _wallets[token].Balance += amount;

            return _wallets[token].Balance;
        }

        public double Withdraw(string token, double amount)
        {
            if (_wallets.Keys.Contains(token))
            {
                throw new Exception("The wallet for same account has been created");
            }

            if (_wallets[token].Balance < amount)
            {
                throw new Exception("The withdraw amount is more than the wallet balance");
            }

            _wallets[token].Balance -= amount;

            return _wallets[token].Balance;
        }
    }
}
