using System;
using System.Collections.Generic;
using System.Text;

namespace Task1_OOP3_WPF
{
    /// <summary>
    /// Интерфейс, который содержит 3 метода для работы с банковскими счетами
    /// </summary>
    public interface IMethodsForBankOperations
    {
        void AddBankAccount(BankAccountNames name, double money);
        bool RemoveBankAccount(int accountId);
        bool TransferMoneyBetweenAccounts(int accountIdFrom, int accountIdTo, double sum);
    }
}
