using System;
using System.Collections.Generic;
using System.Text;

namespace Task1_Events_WPF
{
    // Создаём интерфейс для контравариантности
    public interface ITransferBankAccountMoney<in T>
        where T : BankAccount
    {
        bool TransferMoney(T bankAccountFrom, T bankAccountTo, double money);
    }
}
