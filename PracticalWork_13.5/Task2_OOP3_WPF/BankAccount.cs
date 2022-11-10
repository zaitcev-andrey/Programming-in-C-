using System;
using System.Collections.Generic;
using System.Text;

namespace Task2_OOP3_WPF
{
    // Специально делаем этот класс абстрактным, так как в нём не хватает функционала для использования
    abstract public class BankAccount
    {
        public int Id { get; }
        public string Name { get; }
        public double InterestRate { get; set; }
        public double Money { get; set; }
        public DateTime DateOfOpening { get; }
        public DateTime DateOfClosing { get; set; }

        protected static int maxId;
        static BankAccount()
        {
            maxId = 0;
        }

        public BankAccount(int id, string name, double money)
        {
            maxId++;

            Id = id;
            Name = name;
            Money = money;
            DateOfOpening = DateTime.Now;
        }

        public int GetMaxId()
        {
            return maxId;
        }
    }
}
