using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountLibrary
{
    public class MoneyException : Exception
    {
        public MoneyException(string Msg) : base(Msg) { }
    }
}
