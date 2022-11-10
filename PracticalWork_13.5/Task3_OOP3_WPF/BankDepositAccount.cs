using System;
using System.Collections.Generic;
using System.Text;

namespace Task3_OOP3_WPF
{
    public enum BankDepositAccountNames
    {
        FirstAccount,
        NewTimeAccount,
        StableAccount
    }

    public class BankDepositAccount : BankAccount
    {
        private static Dictionary<BankDepositAccountNames, double> allInterestRate = new Dictionary<BankDepositAccountNames, double>();
        static BankDepositAccount()
        {
            maxId = 0;

            allInterestRate[BankDepositAccountNames.FirstAccount] = 5.5;
            allInterestRate[BankDepositAccountNames.NewTimeAccount] = 6;
            allInterestRate[BankDepositAccountNames.StableAccount] = 5;
        }

        /// <summary>
        /// Конструктор по созданию вклада
        /// </summary>
        /// <param name="name">Имя вклада, выбирается из заданных</param>
        /// <param name="money">Деньги, которые будут положены на вклад</param>
        public BankDepositAccount(int id, BankDepositAccountNames name, double money) : base(id, name.ToString(), money)
        {
            InterestRate = allInterestRate[name];
            DateOfClosing = GetDateOfClosing(name, DateOfOpening);
        }

        private DateTime GetDateOfClosing(BankDepositAccountNames name, DateTime dateOfOpening)
        {
            switch (name)
            {
                case BankDepositAccountNames.FirstAccount:
                    return dateOfOpening.AddYears(1);
                case BankDepositAccountNames.NewTimeAccount:
                    return dateOfOpening.AddMonths(4);
                default:
                    // в случае StableAccount
                    return dateOfOpening.AddYears(2);
            }
        }
    }
}
