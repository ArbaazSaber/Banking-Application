using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static BankingApp.Common.TransactionEventArgs;

namespace BankingApp.Process
{
    public interface IAccount
    {
        void Deposit(double amount);
        void Withdraw(double amount);
        void FundTransfer(IAccount targetAccount, double amount);

        event TransactionEventHandler DepositEvent;
        event TransactionEventHandler WithdrawEvent;
        // string toString();

    }
}
