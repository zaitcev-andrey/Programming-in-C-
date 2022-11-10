using System;
using System.Collections.Generic;
using System.Text;

namespace Task3_OOP3_WPF
{
    // Создаём интерфейс для контравариантности
    public interface ITransferBankAccountMoney<in T>
        where T : BankAccount
    {
        bool TransferMoney(T bankAccountFrom, T bankAccountTo, double money);
    }
}
