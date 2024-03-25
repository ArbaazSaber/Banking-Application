using Banking.DataAcesss;
using BankingApp.Common;
using BankingApp.DataAcesss;
using BankingApp.Entities;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Process
{
    public class BankingProcess
    {
        public BankingProcess() { }
        public AccountInfo CreateNewAccount(int id, string name, AccountType type, double amount)
        {
            AccountInfoCollection collection = new AccountInfoCollection();
            AccountInfoFileCollection fileCollection = new AccountInfoFileCollection();
            AccountDataAccess access = new AccountDataAccess();


            //  AccountInfoFileCollection fileCollection = new AccountInfoFileCollection();
            //ArrayList list= new ArrayList();
            var accInfo = collection.FindAccountBy(id);
            if (accInfo == null)
            {
                var acc = AccountFactory.Create(id, name, type, amount);
                if (acc is null)
                {
                    return null;
                }
                accInfo = new AccountInfo(id, name, type, amount);

                collection.AddNew(accInfo);
                fileCollection.AddNew(accInfo);

            }
            return accInfo;
        }
        public AccountInfo GetAccountByID(int id)
        {
            //   AccountInfoCollection collection = new AccountInfoCollection();
            AccountInfoFileCollection accountInfoFileCollection = new AccountInfoFileCollection();
            AccountDataAccess access = new AccountDataAccess();

            AccountInfo temp = accountInfoFileCollection.FindAccountBy(id);
            temp = access.FindAccountBy(id);
            return temp;
        }
        public void DeleteAccountByID(int id)
        {
            AccountInfoCollection collection = new AccountInfoCollection();
            var acc = collection.FindAccountBy(id);
            if (acc is null) return;

            collection.RemoveAccount(acc);

        }
        public AccountInfo ExecuteTransaction(int id, int toId, double amount, int transactionType)
        {
            AccountInfoCollection collection = new AccountInfoCollection();
            //var acc = collection.FindAccountBy(id);
            if (transactionType == 1)
            {
                return collection.MoneyDeposit(id, amount);
            }
            else if (transactionType == 2)
            {
                return collection.MoneyWithdraw(id, amount);
            }
            else if (transactionType == 3)
            {
                return collection.FundTransfer(id, toId, amount);
            }
            return null;


        }

        public ArrayList GetAllAccounts()
        {
            AccountInfoFileCollection accountInfoFileCollection = new AccountInfoFileCollection();
            //  AccountInfoCollection collection =new AccountInfoCollection();

            return accountInfoFileCollection.GetAllAccounts();
        }
        public AccountInfo GetUpdateAccount(int id, string name)
        {

            AccountInfoCollection collection = new AccountInfoCollection();
            AccountInfo acc = collection.FindAccountBy(id);

            if (name != acc.Name)
            {
                if (name.Length == 0) name = acc.Name;
            }
            /* AccountType type = 0;
             if (accType.Length > 0)
             {
                 type = (AccountType)(int.Parse(accType));
             }*/
            //  if ((int)type == 0) type=acc.Type; 
            var temp = AccountFactory.Create(id, name, acc.Type, acc.Balance);
            if (temp is null) return null;
            AccountInfo accInfo = new AccountInfo(id, name, acc.Type, acc.Balance);
            acc = collection.UpdateAccount(accInfo);
            return acc;
        }
    }
}
