using System;
using System.Collections.Generic;
using System.Text;

namespace Task2_OOP3_WPF
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
