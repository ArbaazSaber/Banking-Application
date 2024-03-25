using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Common
{
    public class TransactionEventArgs : System.EventArgs
    {
        public int AccountId { get; set; }
        public double Amount { get; set; }
        public string TransactionType { get; set; } = "";
        public DateTime TransactionDate { get; set; } = DateTime.Now;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"An amount of Rs. {Amount:N2} was ")
                .Append($"{(TransactionType.ToLower() == "deposit" ? "deposited into" : "withdrawn from")}")
                .Append($" your account {AccountId:000000} ")
                .Append($" on {TransactionDate:f}");
            return sb.ToString();
        }
        public delegate void TransactionEventHandler(object sender, TransactionEventArgs e);
    }
}
