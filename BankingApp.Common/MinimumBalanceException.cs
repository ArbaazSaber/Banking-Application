using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Common
{
    public class MinimumBalanceException:Exception
    {
        public MinimumBalanceException() : base() { }
        public MinimumBalanceException(string message) : base(message) { }
        public MinimumBalanceException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
