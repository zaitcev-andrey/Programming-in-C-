using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountLibrary
{
    // Здесь мы создаём метод расширения для класса банковских счетов
    public static class ExtendBankAccount
    {
        public static void AddMoney<T>(this T bankAccount, double money)
            where T : BankAccountLibrary.BankAccount
        {
            bankAccount.Money += money;
        }

        public static void SubtractMoney<T>(this T bankAccount, double money)
            where T : BankAccountLibrary.BankAccount
        {
            bankAccount.Money -= money;
        }
    }
}
