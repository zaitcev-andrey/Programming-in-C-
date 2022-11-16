using System;
using System.Collections.Generic;
using System.Text;

namespace BankAccountLibrary
{
    public enum BankNotDepositAccountNames
    {
        BeneficialAccount,
        NewLifeAccount,
        SafeAccount
    }

    public class BankNotDepositAccount : BankAccount
    {
        private static Dictionary<BankNotDepositAccountNames, double> allInterestRate = new Dictionary<BankNotDepositAccountNames, double>();
        static BankNotDepositAccount()
        {
            maxId = 0;

            allInterestRate[BankNotDepositAccountNames.BeneficialAccount] = 6.5;
            allInterestRate[BankNotDepositAccountNames.NewLifeAccount] = 5.3;
            allInterestRate[BankNotDepositAccountNames.SafeAccount] = 4.8;
        }

        /// <summary>
        /// Конструктор по созданию вклада
        /// </summary>
        /// <param name="name">Имя вклада, выбирается из заданных</param>
        /// <param name="money">Деньги, которые будут положены на вклад</param>
        public BankNotDepositAccount(int id, BankNotDepositAccountNames name, double money) : base(id, name.ToString(), money)
        {
            InterestRate = allInterestRate[name];
            DateOfClosing = GetDateOfClosing(name, DateOfOpening);
        }
        // перегружаем оператор +
        public static BankNotDepositAccount operator +(BankNotDepositAccount bankAccount, double money_)
        {
            Enum.TryParse(bankAccount.Name, out BankNotDepositAccountNames name);
            return new BankNotDepositAccount(bankAccount.Id, name, bankAccount.Money + money_);
        }
        private DateTime GetDateOfClosing(BankNotDepositAccountNames name, DateTime dateOfOpening)
        {
            switch (name)
            {
                case BankNotDepositAccountNames.BeneficialAccount:
                    return dateOfOpening.AddMonths(4);
                case BankNotDepositAccountNames.NewLifeAccount:
                    return dateOfOpening.AddYears(2);
                default:
                    // в случае SafeAccount
                    return dateOfOpening.AddYears(1);
            }
        }
    }
}
