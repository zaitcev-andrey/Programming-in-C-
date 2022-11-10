using System;
using System.Collections.Generic;
using System.Text;

namespace Task3_OOP3_WPF
{
    // Реализуем контравариантный интерфейс
    public class Realize_ITransferBankAccountMoney<T> : ITransferBankAccountMoney<T>
        where T : BankAccount
    {
        public bool TransferMoney(T bankAccountFrom, T bankAccountTo, double money)
        {
            if(bankAccountFrom.Money >= money)
            {
                bankAccountFrom.Money -= money;
                bankAccountTo.Money += money;
                return true;
            }
            return false;
        }
    }
}
