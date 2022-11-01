using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Task1_OOP2_WPF
{
    enum VariantsOfSorts
    {
        FirstName,
        LastName,
        MiddleName,
        Age,
        Id
    }

    internal class Client
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public int Age { get; set; }
        public string TelephoneNumber { get; set; }
        public string Pasport { get; set; }
        public int DepartmentId { get; set; }

        public DateTime dateTime { get; set; }
        public string WhoChangedData { get; set; }
        public string WhatDataIsChange { get; set; }
        private string LogAboutChanges; // скроем лог информации, чтобы в файле json он не дублировался

        // в этой статической будет храниться максимальный id клиентов
        private static int id;

        // а в этом Id будет храниться свой собственный id у каждого клиента
        public int Id { get; set; }
        static Client()
        {
            id = 0;
        }

        // Этот метод понадобился, если из режима консультанта после загрузки данных
        // мы переходим в режим менеджера, где снова загружаем данные. Нужно, чтобы
        // id клиентов не рос дальше, а начинался заново
        public static void ResetId()
        {
            id = 0;
        }

        public Client(string firstName, string lastName,
            string middleName, int age, string telephoneNumber,
            string pasport, int departmentId, int numberInDepartment,
            string whoChangedData = "",
            string whatDataIsChange = "")
        {
            id++;
            this.Id = numberInDepartment;

            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            Age = age;
            TelephoneNumber = telephoneNumber;
            Pasport = pasport;
            DepartmentId = departmentId;
            dateTime = DateTime.Now;
            WhoChangedData = whoChangedData;
            WhatDataIsChange = whatDataIsChange;
        }

        // конструктор по умолчанию
        public Client() : this("", "", "", 0, "", "", 0, 0) { }

        public void SaveChanges(string dateTime, string WhoChangedData, string WhatDataIsChange)
        {
            this.dateTime = DateTime.Parse(dateTime);
            this.WhoChangedData = WhoChangedData;
            this.WhatDataIsChange = WhatDataIsChange;

            StringBuilder sb = new StringBuilder();
            sb.Append($"Время изменения записи: {dateTime}");
            sb.Append($"\nКакие данные изменены: {WhatDataIsChange}");
            sb.Append($"\nКто изменил данные: {WhoChangedData}");
            LogAboutChanges = sb.ToString();
        }

        public void CheckChanges()
        {
            if (string.IsNullOrEmpty(LogAboutChanges))
                Console.WriteLine("Изменений пока что нет");
            else
                Console.WriteLine("Последние изменения:\n" + LogAboutChanges);
        }

        public string GetLastChanges()
        {
            if (string.IsNullOrEmpty(LogAboutChanges))
                return "Изменений пока что нет";
            else
                return ("Последние изменения:\n" + LogAboutChanges);
        }

        public void PrintFio()
        {
            Console.WriteLine($"ФИО клиента: {LastName} {FirstName} {MiddleName}");
        }

        public string GetInfoForConsultantAboutPasport()
        {
            if (string.IsNullOrEmpty(Pasport))
                return "Данных о паспорте нет!";
            else
                return $"Серия и номер паспорта у клиента {LastName} {FirstName} {MiddleName} скрыты: **** ******";
        }

        // Теперь Sort у списка клиентов будет знать, как сортировать их
        public static IComparer<Client> SortBy(VariantsOfSorts choice)
        {
            switch(choice)
            {
                case VariantsOfSorts.FirstName:
                    return new SortByFirstName();
                case VariantsOfSorts.LastName:
                    return new SortByLastName();
                case VariantsOfSorts.MiddleName:
                    return new SortByMiddleName();
                case VariantsOfSorts.Age:
                    return new SortByAge();
                default:
                    return new SortById();
            }
        }

        #region Все сортировки
        private class SortByFirstName : IComparer<Client>
        {
            public int Compare([AllowNull] Client x, [AllowNull] Client y)
            {
                return string.Compare(x.FirstName, y.FirstName, StringComparison.OrdinalIgnoreCase);
            }
        }
        private class SortByLastName : IComparer<Client>
        {
            public int Compare([AllowNull] Client x, [AllowNull] Client y)
            {
                return string.Compare(x.LastName, y.LastName);
            }
        }
        private class SortByMiddleName : IComparer<Client>
        {
            public int Compare([AllowNull] Client x, [AllowNull] Client y)
            {
                return string.Compare(x.MiddleName, y.MiddleName);
            }
        }
        private class SortByAge : IComparer<Client>
        {
            public int Compare([AllowNull] Client x, [AllowNull] Client y)
            {
                // Сортировка по возрастанию
                if (x.Age >= y.Age) return 1;
                else return -1;
            }
        }
        private class SortById : IComparer<Client>
        {
            public int Compare([AllowNull] Client x, [AllowNull] Client y)
            {
                // Сортировка по возрастанию
                if (x.Id >= y.Id) return 1;
                else return -1;
            }
        }
        #endregion

    }
}
