using BankingApp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Entities
{
    public class AccountInfo
    {
        private int _id;
        private string _name;
        private AccountType _type;
        private double _balance;

        private AccountInfo() { }

        public AccountInfo(int id, string name, AccountType type, double balance)
        {
            _id = id;
            _name = name;
            _type = type;
            _balance = balance;
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public AccountType Type
        {
            get { return _type; }
            set { _type = value; }
        }
        public double Balance
        {
            get { return _balance; }
            set { _balance = value; }
        }
    }
}