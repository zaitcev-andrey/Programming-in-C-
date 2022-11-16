using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel; // INotifyPropertyChanged

namespace BankAccountLibrary
{
    // Специально делаем этот класс абстрактным, так как в нём не хватает функционала для использования
    // Реализуем в нём событие для обновления money в формочке WPF
    abstract public class BankAccount : INotifyPropertyChanged
    {
        public int Id { get; }
        public string Name { get; }
        public double InterestRate { get; set; }
        private double money;
        public double Money 
        {
            get { return money; }
            set
            {
                money = value;
                // с этим событием на форме WPF у нас будут происходить изменения без явного написания Refresh()
                // но данное решение будет работать только если поле Money привязано через Binding
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Money)));
            } 
        }
        public DateTime DateOfOpening { get; }
        public DateTime DateOfClosing { get; set; }

        protected static int maxId;

        public event PropertyChangedEventHandler PropertyChanged; // интерфейс добавил это событие

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
