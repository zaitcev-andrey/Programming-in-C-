using System;
using System.Collections.Generic;
using System.Text;

namespace Task1_OOP3_WPF
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
        public List<BankAccount> BankAccounts { get; set; }

        // В этой переменной будет храниться максимальный id счёта у клиента
        public int MaxBankAccountsId { get; set; }

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
            int age, string telephone, string pasport, List<BankAccount> bankAccounts = null)
        {
            if (bankAccounts == null)
                MaxBankAccountsId = 0;
            else MaxBankAccountsId = bankAccounts.Count;

            maxId++;

            Id = maxId;
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            Age = age;
            Telephone = telephone;
            Pasport = pasport;
            FreeBalance = 0;
            BankAccounts = bankAccounts;
        }
        
        public int GetMaxId()
        {
            return maxId;
        }

        public void AddBankAccount(BankAccountNames name, double money)
        {
            MaxBankAccountsId++;
            BankAccounts.Add(new BankAccount(MaxBankAccountsId, name, money));
        }

        public bool RemoveBankAccount(int accountId)
        {
            // сначала проверяем, что по данному id счёта действительно есть счёт
            bool flag = true;
            int ind = 0;
            for (int i = 0; i < BankAccounts.Count; i++)
            {
                if (BankAccounts[i].Id == accountId)
                {
                    ind = i;
                    flag = false;
                    break;
                }
            }
            if (flag)
                return false; // не удалось закрыть счёт, так как его нет

            // сначала до удаления переносим деньги со счёта на свободный баланс клиента
            FreeBalance += BankAccounts[ind].Money;
            // а теперь спокойно удаляем счёт
            BankAccounts.RemoveAt(ind);
            return true;
        }

        public bool TransferMoneyBetweenAccounts(int accountIdFrom, int accountIdTo, double sum)
        {
            // Нужно получить id, соответствующие индексам в списке
            int idFrom = 0;
            int idTo = 0;
            bool f1, f2;
            f1 = f2 = false;
            for (int i = 0; i < BankAccounts.Count; i++)
            {
                if (f1 && f2)
                    break;
                if (BankAccounts[i].Id == accountIdFrom)
                {
                    idFrom = i;
                    f1 = true;
                }

                else if (BankAccounts[i].Id == accountIdTo)
                {
                    idTo = i;
                    f2 = true;
                }
            }

            if (!(f1 && f2))
                return false; // либо первый, либо второй, либо оба id счетов введены неверно

            // проверяем сумму на корректность
            if (sum <= 0 || sum > BankAccounts[idFrom].Money)
                return false;

            // переносим деньги
            BankAccounts[idTo].Money += sum;
            // обновляем деньги на счёте, с которого их перенесли
            BankAccounts[idFrom].Money -= sum;
            return true;
        }
    }
}
