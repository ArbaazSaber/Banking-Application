using Banking.DataAcesss;
using BankingApp.Common;
using BankingApp.Entities;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Process
{
    public class AccountFactory
    {
        static AccountInfoCollection collection = new AccountInfoCollection();
        public static IAccount Create(int id, string name, AccountType type, double amount)
        {
            IAccount acc = null!;

            //if (type == AccountType.Savings && amount >= 10000)
            //{
            //    acc = new Savings(id, name, amount);
            //}
            //else if (type == AccountType.Current && amount >= 25000)
            //{
            //    acc = new Current(id, name, amount);
            //}
            //else
            //{
            //    throw new MinimumBalanceException("Insufficient funds to open the Account.");
            //}
            //TODO: else part

            //First find the item in the Collection, if it exists, then extract the item and 
            //send it to the client.

            AccountInfo item = collection.FindAccountBy(id);
            if (item == null)
            {
                if (type == AccountType.Savings && amount >= 10000)
                {
                    item = new AccountInfo(id, name, type, amount);
                }
                else if (type == AccountType.Current && amount >= 25000)
                {
                    item = new AccountInfo(id, name, type, amount);
                }
                //else
                //{
                //    throw new MinimumBalanceException("Insufficient funds to open the Account.");
                //}
                //collection.AddNew(item);
            }
            if (item!.Type == AccountType.Savings)
            {
                acc = new Savings(item);
            }
            else if (item.Type == AccountType.Current)
            {
                acc = new Current(item);
            }
            //If it does not exist, then create a new accountInfo based on the parameters sent 
            //and update the code block constructors by passing the accountInfo object.
            //Insert the newly created accountInfo into the array.

            return acc;
        }
    }
}
