using System;
using System.Collections.Generic;
using System.Text;
using BankAccountLibrary;

namespace Task1_Exceptions_WPF
{
    // реализуем ковариантный интерфейс
    public class Realize_IBankDepositAccount : IBankDepositAccount<BankDepositAccount>
    {
        public BankDepositAccount GetBankDepositAccount(int id, BankDepositAccountNames name, double money)
        {
            return new BankDepositAccount(id, name, money);
        }
    }

    public class Realize_IBankNotDepositAccount : IBankNotDepositAccount<BankNotDepositAccount>
    {
        public BankNotDepositAccount GetBankNotDepositAccount(int id, BankNotDepositAccountNames name, double money)
        {
            return new BankNotDepositAccount(id, name, money);
        }
    }
}
