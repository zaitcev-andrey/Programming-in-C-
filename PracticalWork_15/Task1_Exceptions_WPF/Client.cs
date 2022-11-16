using System;
using System.Collections.Generic;
using System.Text;
using BankAccountLibrary;

namespace Task1_Exceptions_WPF
{
    public class Client : IMethodsForBankOperations
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public int Age { get; set; }
        public string Telephone { get; set; }
        public string Pasport { get; set; }

        // нужен для накопления денег после закрытия вклада
        public double FreeBalance { get; set; }

        // В отличие от задания 1 здесь списки у нас состоят из базового BankAccount
        // но в 1 список мы будем помещать депозитные счета, а во 2 недепозитные
        // достигается это при помощи ковариантности
        public List<BankAccount> BankDepositAccounts { get; set; }
        public List<BankAccount> BankNotDepositAccounts { get; set; }

        // В этой переменной будет храниться максимальный id счёта у клиента
        public int MaxBankDepositAccountsId { get; set; }
        public int MaxBankNotDepositAccountsId { get; set; }

        private static int maxId; // для подсчёта клиентов

        static Client()
        {
            maxId = 0;
        }
        /// <summary>
        /// Конструктор по созданию клиента
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="middleName"></param>
        /// <param name="age"></param>
        /// <param name="telephone"></param>
        /// <param name="pasport"></param>
        /// <param name="bankAccounts">Счета клиента,
        /// по умолчанию их нет, так как не у всех клиентов есть счета</param>
        public Client(string firstName, string lastName, string middleName,
            int age, string telephone, string pasport,
            List<BankAccount> bankDepositAccounts = null,
            List<BankAccount> bankNotDepositAccounts = null)
        {
            if (bankDepositAccounts == null)
                MaxBankDepositAccountsId = 0;
            else MaxBankDepositAccountsId = bankDepositAccounts.Count;

            if (bankNotDepositAccounts == null)
                MaxBankNotDepositAccountsId = 0;
            else MaxBankNotDepositAccountsId = bankNotDepositAccounts.Count;

            maxId++;

            Id = maxId;
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            Age = age;
            Telephone = telephone;
            Pasport = pasport;
            FreeBalance = 0;
            BankDepositAccounts = bankDepositAccounts;
            BankNotDepositAccounts = bankNotDepositAccounts;
        }
        
        public int GetMaxId()
        {
            return maxId;
        }

        /// <summary>
        /// Данный метод нужен для того, чтобы не дублировать код в остальных методах
        /// нам достаточно 1 раз в начале определить, с каким счётом мы будем работать,
        /// и затем уже подставлять этот счёт в остальной функционал дальше
        /// всё работает через ссылки на исходные BankDepositAccounts и BankNotDepositAccounts
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private List<BankAccount> GetProperListOfBankAccounts(EnumBankAccountTypes type)
        {
            List<BankAccount> bankAccounts;
            if (type == EnumBankAccountTypes.Deposit)
                bankAccounts = BankDepositAccounts;
            else
                bankAccounts = BankNotDepositAccounts;
            return bankAccounts;
        }

        public int GetCorrectAccountId(EnumBankAccountTypes type, int accountId)
        {
            List<BankAccount> properBankAccounts = GetProperListOfBankAccounts(type);
            int ind = -1;
            for (int i = 0; i < properBankAccounts.Count; i++)
            {
                if (properBankAccounts[i].Id == accountId)
                {
                    ind = i;
                    break;
                }
            }
            return ind;
        }

        public void AddBankAccount(EnumBankAccountTypes type, string name, double money)
        {
            if(type == EnumBankAccountTypes.Deposit)
            {
                if (!Enum.TryParse(name, out BankDepositAccountNames correctName))
                    return;
                MaxBankDepositAccountsId++;
                BankDepositAccounts.Add(new BankDepositAccount(MaxBankDepositAccountsId, correctName, money));
            }
                
            else if(type == EnumBankAccountTypes.NotDeposit)
            {
                if (!Enum.TryParse(name, out BankNotDepositAccountNames correctName))
                    return;
                MaxBankNotDepositAccountsId++;
                BankNotDepositAccounts.Add(new BankNotDepositAccount(MaxBankNotDepositAccountsId, correctName, money));
            }
        }

        public bool RemoveBankAccount(EnumBankAccountTypes type, int accountId)
        {
            // сначала проверяем, что по данному id счёта действительно есть счёт
            int ind = GetCorrectAccountId(type, accountId);
            if(ind == -1)
                return false; // не удалось закрыть счёт, так как его нет
            List<BankAccount> properBankAccounts = GetProperListOfBankAccounts(type);

            // сначала до удаления переносим деньги со счёта на свободный баланс клиента
            FreeBalance += properBankAccounts[ind].Money;
            // а теперь спокойно удаляем счёт
            properBankAccounts.RemoveAt(ind);
            return true;
        }

        public bool TransferMoneyBetweenAccounts(EnumBankAccountTypes type, int accountIdFrom, int accountIdTo, double sum)
        {
            int idFrom = GetCorrectAccountId(type, accountIdFrom);
            int idTo = GetCorrectAccountId(type, accountIdTo);
            if (idFrom == -1 || idTo == -1)
                return false; // либо первый, либо второй, либо оба id счетов введены неверно
            List<BankAccount> properBankAccounts = GetProperListOfBankAccounts(type);

            // проверяем сумму на корректность
            if (sum <= 0 || sum > properBankAccounts[idFrom].Money)
                return false;

            // Для теста используем ниже метод расширения для пополнения и вычитания средств на счёте
            // переносим деньги
            properBankAccounts[idTo].AddMoney(sum);
            //properBankAccounts[idTo].Money += sum;

            // обновляем деньги на счёте, с которого их перенесли
            properBankAccounts[idFrom].SubtractMoney(sum);
            //properBankAccounts[idFrom].Money -= sum;
            return true;
        }

        public bool AddMoneyToBankAccount(EnumBankAccountTypes type, int accountId, double money)
        {
            // сначала проверяем, что по данному id счёта действительно есть счёт
            int ind = GetCorrectAccountId(type, accountId);
            if (ind == -1)
                return false;
            List<BankAccount> properBankAccounts = GetProperListOfBankAccounts(type);

            // Для теста используем ниже метод расширения для пополнения средств на счёте
            properBankAccounts[ind].AddMoney(money);
            //properBankAccounts[ind].Money += money; // аналогичная запись

            return true;
        }

        public List<BankAccount> GetBankAccountsList(EnumBankAccountTypes type)
        {
            // возвращаем ссылку на банковский счёт
            if (type == EnumBankAccountTypes.Deposit)
                return this.BankDepositAccounts;
            else
                return this.BankNotDepositAccounts;
        }
    }
}
