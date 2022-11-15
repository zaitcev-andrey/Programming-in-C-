using System;
using System.Collections.Generic;
using System.Text;

namespace Task1_Events_WPF
{
    // Интерфейс для ковариантности
    public interface IBankDepositAccount<out T>
        where T : BankAccount
    {
        T GetBankDepositAccount(int id, BankDepositAccountNames name, double money);
    }

    public interface IBankNotDepositAccount<out T>
        where T : BankAccount
    {
        T GetBankNotDepositAccount(int id, BankNotDepositAccountNames name, double money);
    }
}
