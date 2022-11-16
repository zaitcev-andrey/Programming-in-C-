using System;
using System.Collections.Generic;
using System.Text;
using BankAccountLibrary;

namespace Task1_Exceptions_WPF
{
    // Реализуем контравариантный интерфейс
    public class Realize_ITransferBankAccountMoney<T> : ITransferBankAccountMoney<T>
        where T : BankAccount
    {
        public bool TransferMoney(T bankAccountFrom, T bankAccountTo, double money)
        {
            if(bankAccountFrom.Money >= money)
            {
                // используем методы расширения
                bankAccountFrom.SubtractMoney(money);
                bankAccountTo.AddMoney(money);
                return true;
            }
            return false;
        }
    }
}
