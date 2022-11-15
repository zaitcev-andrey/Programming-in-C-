using System;
using System.Collections.Generic;

namespace Task1_Events_WPF
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
        public event Action<string> bankOperation;

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
            // В этом методе и дальше мы будем сохранять в событие сообщение
            // это сообщение сразу передастся подписчику
            bankOperation?.Invoke($"Произошло открытие {type.ToString()} счёта под именем {name} " +
                    $"у пользователя {memberId}. Время {DateTime.Now.ToShortTimeString()}");
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
            if(Members[memberId - 1].RemoveBankAccount(type, accountId))
            {
                bankOperation?.Invoke($"Произошло закрытие {type.ToString()} счёта под номером {accountId} " +
                    $"у пользователя {memberId}. Время {DateTime.Now.ToShortTimeString()}");
                return true;
            }
            return false;
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
            if(Members[memberId - 1].TransferMoneyBetweenAccounts(type, accountIdFrom, accountIdTo, sum))
            {
                bankOperation?.Invoke($"Произошёл перенос {sum} рублей у пользователя {memberId} с {type.ToString()} счёта под номером " +
                    $"{accountIdFrom} на {type.ToString()} счёт под номером {accountIdTo}. Время {DateTime.Now.ToShortTimeString()}");
                return true;
            }
            return false;
        }

        public bool AddMoneyToBankAccount(EnumBankAccountTypes type, int memberId, int accountId, double money)
        {
            if(Members[memberId - 1].AddMoneyToBankAccount(type, accountId, money))
            {
                bankOperation?.Invoke($"Произошло добавление {money} рублей пользователю {memberId} на {type.ToString()}" +
                    $" счёт под номером {accountId}. Время {DateTime.Now.ToShortTimeString()}");
                return true;
            }
            return false;
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
                if(transferMoney.TransferMoney((BankDepositAccount)bankAccountsFrom[idFrom], (BankDepositAccount)bankAccountsTo[idTo], money))
                {
                    bankOperation?.Invoke($"Произошёл перенос {money} рублей от пользователя {memberIdFrom} с {type.ToString()} счёта под номером " +
                        $"{accountIdFrom} к пользователю {memberIdTo} на {type.ToString()} счёт под номером {accountIdTo}. Время {DateTime.Now.ToShortTimeString()}");
                    return true;
                }
                return false;
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
