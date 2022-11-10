using System.Collections.Generic;

namespace Task3_OOP3_WPF
{
    /// <summary>
    /// Этот класс представляет банк, который умеет работать с банковскими счетами
    /// </summary>
    /// <typeparam name="T">В качестве данных класса "Банк" могут быть только такие классы,
    /// которые реализуют интерфейс с тремя методами по работе с банковскими счетами</typeparam>
    public class Bank<T>
        where T : IMethodsForBankOperations
    {
        public List<T> Members { get; set; }

        public Bank(List<T> members)
        {
            Members = members;
        }

        /// <summary>
        /// Метод на открытие счёта у клиента
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="name"></param>
        /// <param name="money"></param>
        public void OpenBankAccount(EnumBankAccountTypes type, int memberId, string name, double money)
        {
            // уменьшили id на 1, так как отсчёт в WPF идёт с 1
            Members[memberId - 1].AddBankAccount(type, name, money);
        }

        /// <summary>
        /// Метод на закрытие счёта у клиента
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="accountId"></param>
        public bool CloseBankAccount(EnumBankAccountTypes type, int memberId, int accountId)
        {
            // тут мы используем метод из интерфейса - удаляем счёт
            // если получилось удалить, то вернётся true, а иначе false
            return Members[memberId - 1].RemoveBankAccount(type, accountId);
        }

        /// <summary>
        /// Метод переноса денег со счёта на счёт. При успехе вернёт true, при неудаче false
        /// </summary>
        /// <param name="memberId">id клиента, со счетами которого будем работать</param>
        /// <param name="accountIdFrom">id счёта откуда будут переноситься деньги</param>
        /// <param name="accountIdTo">id счёта куда деньги придут</param>
        /// <param name="sum">Сумма денег, которую нужно перенести</param>
        public bool TransferMoneyFromOneAccountToAnother(EnumBankAccountTypes type, int memberId, int accountIdFrom, int accountIdTo, double sum)
        {
            // также вызываем метод из интерфейса
            // если получилось перенести деньги, то вернётся true, а иначе false
            return Members[memberId - 1].TransferMoneyBetweenAccounts(type, accountIdFrom, accountIdTo, sum);
        }

        public bool AddMoneyToBankAccount(EnumBankAccountTypes type, int memberId, int accountId, double money)
        {
            return Members[memberId - 1].AddMoneyToBankAccount(type, accountId, money);
        }

        public bool TransferMoneyFromOneMemberToAnother(int memberIdFrom, int memberIdTo, EnumBankAccountTypes type,
            int accountIdFrom, int accountIdTo, double money)
        {
            // Получаем действительные id банковских счетов
            int idFrom = Members[memberIdFrom - 1].GetCorrectAccountId(type, accountIdFrom);
            int idTo = Members[memberIdTo - 1].GetCorrectAccountId(type, accountIdTo);
            if(idFrom == -1 || idTo == -1)
                return false;

            List<BankAccount> bankAccountsFrom = Members[memberIdFrom - 1].GetBankAccountsList(type);
            List<BankAccount> bankAccountsTo = Members[memberIdTo - 1].GetBankAccountsList(type);

            // А теперь используем метод, созданный на основе контравариантности
            if (type == EnumBankAccountTypes.Deposit)
            {
                // Тут происходит контравариантность: в более конкретный тип кладём более общий
                ITransferBankAccountMoney<BankDepositAccount> transferMoney = new Realize_ITransferBankAccountMoney<BankAccount>();
                return transferMoney.TransferMoney((BankDepositAccount)bankAccountsFrom[idFrom], (BankDepositAccount)bankAccountsTo[idTo], money);
            }
            else if (type == EnumBankAccountTypes.NotDeposit)
            {
                ITransferBankAccountMoney<BankNotDepositAccount> transferMoney = new Realize_ITransferBankAccountMoney<BankAccount>();
                return transferMoney.TransferMoney((BankNotDepositAccount)bankAccountsFrom[idFrom], (BankNotDepositAccount)bankAccountsTo[idTo], money);
            }
            
            return true;
        }
    }
}
