using System;
using System.Collections.Generic;
using System.Text;

namespace Task3_OOP3_WPF
{
    /// <summary>
    /// Интерфейс, который содержит 3 метода для работы с банковскими счетами
    /// </summary>
    public interface IMethodsForBankOperations
    {
        void AddBankAccount(EnumBankAccountTypes type, string name, double money);
        bool RemoveBankAccount(EnumBankAccountTypes type, int accountId);
        bool TransferMoneyBetweenAccounts(EnumBankAccountTypes type, int accountIdFrom, int accountIdTo, double sum);
        bool AddMoneyToBankAccount(EnumBankAccountTypes type, int accountId, double money);
        int GetCorrectAccountId(EnumBankAccountTypes type, int accountId);
        List<BankAccount> GetBankAccountsList(EnumBankAccountTypes type);
    }
}
