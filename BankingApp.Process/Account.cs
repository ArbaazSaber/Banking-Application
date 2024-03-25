using BankingApp.Common;
using BankingApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// using System.Transactions;
using static BankingApp.Common.TransactionEventArgs;

namespace BankingApp.Process
{
    public abstract class Account : IAccount
    {
        protected AccountInfo info;
        public event TransactionEventHandler DepositEvent;
        public event TransactionEventHandler WithdrawEvent;


        private Account() { }
        public Account(int id, string name, AccountType type, double amount)
        {

            info = new AccountInfo(id, name, type, amount);

        }
        public Account(AccountInfo pinfo)
        {
            info = pinfo;
        }
        protected void OnDepositEvent(double amount)
        {
            TransactionEventArgs args = new TransactionEventArgs
            {
                AccountId = info.Id,
                Amount = amount,
                TransactionDate = DateTime.Now,
                TransactionType = "deposit"
            };
            DepositEvent?.Invoke(this, args);
        }
        protected void OnWithdrawEvent(double amount)
        {
            TransactionEventArgs args = new TransactionEventArgs
            {
                AccountId = info.Id,
                Amount = amount,
                TransactionDate = DateTime.Now,
                TransactionType = "Withdrawn"
            };
            WithdrawEvent?.Invoke(this, args);
        }
        public abstract void Deposit(double amount);
        public abstract void Withdraw(double amount);
        /*  {
              if (info.Type == AccountType.Savings && (info.Balance - amount) > 5000)
              {
                  info.Balance -= amount;
              }
              else if (info.Type == AccountType.Current && (info.Balance - amount) > 10000)
              {
                  info.Balance -= amount;
              }


          }*/

        public void FundTransfer(IAccount targetAccount, double amount)
        {
            this.Withdraw(amount);
            targetAccount.Deposit(amount);


        }
        public override string ToString()
        {
            return $"Id: {info.Id}, Name: {info.Name}, Type: {info.Type}, Balance: {info.Balance}";
        }
        ~Account()
        {
            info = null;
            Console.WriteLine("Account.Destructor() called .");
        }


    }

}
