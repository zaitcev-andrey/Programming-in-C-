using System;
using System.Collections.Generic;
using System.Text;

namespace Task1_OOP3_WPF
{
    public enum BankAccountNames
    {
        FirstAccount,
        NewTimeAccount,
        StableAccount
    }

    public class BankAccount
    {
        public int Id { get; }
        public BankAccountNames Name { get; }
        public double InterestRate { get; } // процентная ставка это статическое поле
        public double Money { get; set; } // set для переноса денег между счетами
        public DateTime DateOfOpening { get; }
        public DateTime DateOfClosing { get; }

        private static int maxId;
        private static Dictionary<BankAccountNames, double> allInterestRate = new Dictionary<BankAccountNames, double>();
        static BankAccount()
        {
            maxId = 0;

            allInterestRate[BankAccountNames.FirstAccount] = 5.5;
            allInterestRate[BankAccountNames.NewTimeAccount] = 6;
            allInterestRate[BankAccountNames.StableAccount] = 5;
        }

        /// <summary>
        /// Конструктор по созданию вклада
        /// </summary>
        /// <param name="name">Имя вклада, выбирается из заданных</param>
        /// <param name="money">Деньги, которые будут положены на вклад</param>
        public BankAccount(int id, BankAccountNames name, double money)
        {
            maxId++;

            Id = id;
            Name = name;
            InterestRate = allInterestRate[Name];
            Money = money;
            DateOfOpening = DateTime.Now;
            DateOfClosing = GetDateOfClosing(Name, DateOfOpening);
        }

        private DateTime GetDateOfClosing(BankAccountNames name, DateTime dateOfOpening)
        {
            switch (name)
            {
                case BankAccountNames.FirstAccount:
                    return dateOfOpening.AddYears(1);
                case BankAccountNames.NewTimeAccount:
                    return dateOfOpening.AddMonths(4);
                default:
                    // в случае StableAccount
                    return dateOfOpening.AddYears(2);
            }
        }

        public int GetMaxId()
        {
            return maxId;
        }
    }
}
