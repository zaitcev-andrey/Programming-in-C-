using System.Collections.Generic;

namespace Task1_OOP3_WPF
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
        public void OpenBankAccount(int memberId, BankAccountNames name, double money)
        {
            // уменьшили id на 1, так как отсчёт в WPF идёт с 1
            Members[memberId - 1].AddBankAccount(name, money);
        }

        /// <summary>
        /// Метод на закрытие счёта у клиента
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="accountId"></param>
        public bool CloseBankAccount(int memberId, int accountId)
        {
            // тут мы используем метод из интерфейса - удаляем счёт
            // если получилось удалить, то вернётся true, а иначе false
            return Members[memberId - 1].RemoveBankAccount(accountId);
        }

        /// <summary>
        /// Метод переноса денег со счёта на счёт. При успехе вернёт true, при неудаче false
        /// </summary>
        /// <param name="memberId">id клиента, со счетами которого будем работать</param>
        /// <param name="accountIdFrom">id счёта откуда будут переноситься деньги</param>
        /// <param name="accountIdTo">id счёта куда деньги придут</param>
        /// <param name="sum">Сумма денег, которую нужно перенести</param>
        public bool TransferMoneyFromOneAccountToAnother(int memberId, int accountIdFrom, int accountIdTo, double sum)
        {
            // также вызываем метод из интерфейса
            // если получилось перенести деньги, то вернётся true, а иначе false
            return Members[memberId - 1].TransferMoneyBetweenAccounts(accountIdFrom, accountIdTo, sum);
        }
    }
}
