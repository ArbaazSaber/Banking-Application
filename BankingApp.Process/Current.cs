using BankingApp.Common;
using BankingApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Process
{
    internal class Current : Account
    {
        public Current(AccountInfo info) : base(info) { }

        public Current(int id, string name, double amount) : base(id, name, AccountType.Current, amount) { }

        public override void Withdraw(double amount)
        {
            if ((info.Balance - amount) > 10000)
            {
                info.Balance -= amount;
                OnWithdrawEvent(amount);
            }
            else
            {
                throw new MinimumBalanceException("Insufficient funds in the account. Transaction was cancelled.");
            }
        }
        public sealed override void Deposit(double amount)
        {
            info.Balance += amount;
            OnDepositEvent(amount);
        }
    }
}
