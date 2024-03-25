using BankingApp.Common;
using BankingApp.Entities;
using System.Collections;
using System.Diagnostics;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Security.Principal;

namespace Banking.DataAcesss
{
 
 public class AccountInfoCollection
    {
       // static AccountInfo[] items = new AccountInfo[1];
        static ArrayList list=new ArrayList();
        public void AddNew(AccountInfo item)
        {
            var nextId =(list.Count)+101;
            item.Id = nextId;
            list.Add(item);
            //Resize the array with plus item from the second time onwards 
          /*  if (items[0] is null)
                items[0] = item;
            else
            {
                var length = items.Length;
                var newArray = new AccountInfo[length + 1];
    
               Array.Copy(items, newArray, length);
                newArray[length] = item;
                items = newArray;
            }*/
        }
        public AccountInfo FindAccountBy(int accountId)
        {
            AccountInfo acc = null;
            if(list.Count==0)
            {
                return acc;
            }
            foreach (AccountInfo item in list)
            {
                if (item.Id == accountId) { acc = item; break; }
            }
            return acc;
        }
        public void RemoveAccount(AccountInfo accInfo)
        {
            list.Remove(accInfo);
        }
        public AccountInfo MoneyDeposit(int id, double amount)
        {

            foreach(AccountInfo item in list)
            {
                if (item.Id==id)
                {
                    item.Balance += amount;
                    return item;
                }
            }
            return FindAccountBy(id);
        }
        public AccountInfo MoneyWithdraw(int id, double amount)
        {

            foreach (AccountInfo item in list)
            {
                if (item.Id == id)
                {
                    if (item.Type == AccountType.Savings && (item.Balance - amount) >= 50000)
                    {
                        item.Balance -= amount;
                        return item;
                    }
                    else if (item.Type == AccountType.Current && (item.Balance - amount) >= 10000)
                    {
                        item.Balance -= amount;
                        return item;
                    }
                    else return null;
                }
            }
            return null;

        }
        public AccountInfo FundTransfer(int id,int toId, double amount)
        {
            AccountInfo toAcc=FindAccountBy(toId);
            foreach (AccountInfo item in list)
            {
                if (item.Id == id)
                {
                    if (item.Type == AccountType.Savings && (item.Balance - amount) >= 50000)
                    {
                        item.Balance -= amount;
                        toAcc.Balance += amount;
                        return item;
                    }
                    else if (item.Type == AccountType.Current && (item.Balance - amount) >= 10000)
                    {
                        item.Balance -= amount;
                        toAcc.Balance += amount;
                        return item;
                    }
                    else return null;
                }
            }
            return null;
        }

        public AccountInfo UpdateAccount(AccountInfo accInfo )
        {

            AccountInfo acc = FindAccountBy(accInfo.Id);
           
            
           // AccountInfo temp = AccountFactory.Create(id, name, type, acc.Balance);
                //new AccountInfo(id, name,type,acc.Balance);

           
            int index=list.IndexOf(acc);
            list.Remove(acc);

         list.Insert(index, accInfo);


            return accInfo;

        }
        public ArrayList GetAllAccounts()
        {
            return list;
        }
    }
}
